using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace One1Lion.BlazorComponents.SharedLib {
  // Adapted from GitHub issue #29900 using https://github.com/dotnet/runtime/issues/29900#issuecomment-582678375
  public class NonLoopingJsonConverter : JsonConverterFactory {
    public override bool CanConvert(Type typeToConvert) => true;

    public override JsonConverter CreateConverter(
        Type type,
        JsonSerializerOptions options) {

      JsonConverter converter = (JsonConverter)Activator.CreateInstance(
          typeof(NonLoopingConverterInner<>).MakeGenericType(
              new Type[] { type }),
          BindingFlags.Instance | BindingFlags.Public,
          binder: null,
          args: new object[] { },
          culture: null);

      return converter;
    }

    private class NonLoopingConverterInner<TValue> :
        JsonConverter<TValue> {


      public override TValue Read(
          ref Utf8JsonReader reader, Type typeToConvert,
          JsonSerializerOptions options) {

        return JsonSerializer.Deserialize<TValue>(ref reader, options);
      }


      public override void Write(
          Utf8JsonWriter writer, TValue value,
          JsonSerializerOptions options /*options ignored*/) {

        SafeJsonSerializer.Serialize(value, writer);

      }
    }
  }


  public static class SafeJsonSerializer {

    static readonly MethodInfo SerializeEnumerableMethod = typeof(SafeJsonSerializer).GetMethod("SerializeEnumerable", BindingFlags.Static | BindingFlags.NonPublic);



    public static void Serialize<T>(T obj, Utf8JsonWriter jw) =>
        Serialize(obj, null, jw, new List<int> { });



    static void Serialize<T>(T obj, string propertyName, Utf8JsonWriter jw, List<int> hashCodes, bool isContainerType = false) {

      var jsonValueType = GetJsonValueKind(obj);
      if (jsonValueType == JsonValueKind.Array || jsonValueType == JsonValueKind.Object) {
        var hashCode = obj.GetHashCode();
        if (hashCodes.Contains(hashCode))
          return;
        hashCodes.Add(hashCode);
      }

      if (isContainerType && jsonValueType == JsonValueKind.Null)
        return;

      if (propertyName != null) {
        jw.WritePropertyName(propertyName);
      }


      if (obj != null && obj.GetType().IsEnum) {
        jw.WriteStringValue(Enum.GetName(obj.GetType(), obj));
        return;
      }

      switch (jsonValueType) {
        case JsonValueKind.Undefined:
          if (propertyName == null)
            return;
          jw.WriteNullValue();
          break;
        case JsonValueKind.Null:
          jw.WriteNullValue();
          break;
        case JsonValueKind.True:
          jw.WriteBooleanValue(true);
          break;
        case JsonValueKind.False:
          jw.WriteBooleanValue(false);
          break;
        case JsonValueKind.String:
          var result = JsonSerializer.Serialize(obj).Replace("\u0022", "");
          jw.WriteStringValue(result);
          break;
        case JsonValueKind.Number:
          var num = Convert.ToDecimal(obj);
          jw.WriteNumberValue(num);
          break;
        case JsonValueKind.Array:
          jw.WriteStartArray();
          try {
            List<object> list = new List<object>();
            if (typeof(IEnumerable).IsAssignableFrom(obj.GetType())) {
              IEnumerable items = (IEnumerable)obj;
              foreach (var item in items)
                list.Add(item);
            } else if (obj is IEnumerable<object>)
              foreach (var item in obj as IEnumerable<object>)
                list.Add(item);
            else if (obj is IOrderedEnumerable<object>)
              foreach (var item in obj as IOrderedEnumerable<object>)
                list.Add(item);

            SerializeEnumerable(list, jw, hashCodes);
          } catch {

            //upon failure, use reflection and generic SerializeEnumerable method
            Type[] args = obj.GetType().GetGenericArguments();
            Type itemType = args[0];

            MethodInfo genericM = SerializeEnumerableMethod.MakeGenericMethod(itemType);
            genericM.Invoke(null, new object[] { obj, propertyName, jw, hashCodes });
          }
          jw.WriteEndArray();
          break;
        case JsonValueKind.Object:
          jw.WriteStartObject();
          var type = obj.GetType();
          if (type.IsIDictionary()) {
            var dict = obj as IDictionary;
            foreach (var key in dict.Keys)
              Serialize(dict[key], key.ToString(), jw, hashCodes);
          } else {
            foreach (var prop in type.GetProperties().Where(t => t.DeclaringType.FullName != "System.Linq.Dynamic.Core.DynamicClass")) {
              var containerType = IsContainerType(prop.PropertyType);
              Serialize(prop.GetValue(obj), prop.Name, jw, hashCodes, containerType);
            }
          }
          jw.WriteEndObject();
          break;
        default:
          return;
      }
    }

    static void SerializeEnumerable<T>(IEnumerable<T> obj, Utf8JsonWriter jw, List<int> hashCodes) {
      foreach (var item in obj)
        Serialize(item, null, jw, hashCodes);
    }


    static JsonValueKind GetJsonValueKind(object obj) {
      if (obj == null)
        return JsonValueKind.Null;
      var type = obj.GetType();
      if (type.IsArray)
        return JsonValueKind.Array;
      else if (type.IsIDictionary())
        return JsonValueKind.Object;
      else if (type.IsIEnumerable())
        return JsonValueKind.Array;
      else if (type.IsNumber())
        return JsonValueKind.Number;
      else if (type == typeof(bool)) {
        var bObj = (bool)obj;
        if (bObj)
          return JsonValueKind.True;
        else
          return JsonValueKind.False;
      } else if (type == typeof(string) ||
          type == typeof(DateTime) ||
          type == typeof(DateTimeOffset) ||
          type == typeof(TimeSpan) ||
          type.IsPrimitive
          )
        return JsonValueKind.String;
      else if ((type.GetProperties()?.Length ?? 0) > 0)
        return JsonValueKind.Object;
      else
        return JsonValueKind.Undefined;
    }


    static bool IsContainerType(Type type) {
      if (type.IsArray)
        return true;
      else if (type.IsIDictionary())
        return true;
      else if (type.IsIEnumerable())
        return true;
      else if (type.IsNumber())
        return false;
      else if (type == typeof(bool)) {
        return false;
      } else if (type == typeof(string) ||
          type == typeof(DateTime) ||
          type == typeof(DateTimeOffset) ||
          type == typeof(TimeSpan) ||
          type.IsPrimitive
          )
        return false;
      else if ((type.GetProperties()?.Length ?? 0) > 0)
        return true;
      else if (type == typeof(object))
        return true;
      else
        return false;
    }
  }


  internal static class TypeExtensions {
    internal static bool IsIEnumerable(this Type type) {
      return type != typeof(string) && type.GetInterfaces().Contains(typeof(IEnumerable));
    }
    internal static bool IsIDictionary(this Type type) {
      return
          type.GetInterfaces().Contains(typeof(IDictionary))
          || (type.IsGenericType && typeof(Dictionary<,>).IsAssignableFrom(type.GetGenericTypeDefinition()));
    }
    internal static bool IsNumber(this Type type) {
      return type == typeof(byte)
          || type == typeof(ushort)
          || type == typeof(short)
          || type == typeof(uint)
          || type == typeof(int)
          || type == typeof(ulong)
          || type == typeof(long)
          || type == typeof(decimal)
          || type == typeof(double)
          || type == typeof(float)
          ;
    }
  }
}
