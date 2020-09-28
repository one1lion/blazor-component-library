using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace One1Lion.BlazorComponents.SharedLib {
  public static class Utils {
    public static T SimpleClone<T>(T obj) {
      // If T is one of the known primitive (or primitive-like) types listed below, then simply assign the cloned object to the object to be cloned
      if (typeof(T).GetTypeInfo().IsValueType) { return obj; }
      switch (Type.GetTypeCode(typeof(T))) {
        case TypeCode.Boolean:
        case TypeCode.Byte:
        case TypeCode.DateTime:
        case TypeCode.Decimal:
        case TypeCode.Double:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.SByte:
        case TypeCode.Single:
        case TypeCode.String:
        case TypeCode.UInt16:
        case TypeCode.UInt32:
        case TypeCode.UInt64:
          return obj;
        default:
          // Using NewtonSoft to serialize Json object, then deserialize
          var serialized = JsonConvert.SerializeObject(obj, obj.GetType(), new JsonSerializerSettings() {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ObjectCreationHandling = ObjectCreationHandling.Reuse,
            TypeNameHandling = TypeNameHandling.All
          });
          return (T)JsonConvert.DeserializeObject(serialized, obj.GetType(), new JsonSerializerSettings() {
            ObjectCreationHandling = ObjectCreationHandling.Reuse,
            TypeNameHandling = TypeNameHandling.All
          });
      }
    }

    public static object SimpleClone(object obj, Type objType) {
      // If T is one of the known primitive (or primitive-like) types listed below, then simply assign the cloned object to the object to be cloned
      if (objType.GetTypeInfo().IsValueType) { return obj; }
      switch (Type.GetTypeCode(objType)) {
        case TypeCode.Boolean:
        case TypeCode.Byte:
        case TypeCode.DateTime:
        case TypeCode.Decimal:
        case TypeCode.Double:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.SByte:
        case TypeCode.Single:
        case TypeCode.String:
        case TypeCode.UInt16:
        case TypeCode.UInt32:
        case TypeCode.UInt64:
          return obj;
        default:
          // Using NewtonSoft to serialize Json object, then deserialize
          var serialized = JsonConvert.SerializeObject(obj, obj.GetType(), new JsonSerializerSettings() {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ObjectCreationHandling = ObjectCreationHandling.Reuse,
            TypeNameHandling = TypeNameHandling.All
          });
          return JsonConvert.DeserializeObject(serialized, obj.GetType(), new JsonSerializerSettings() {
            ObjectCreationHandling = ObjectCreationHandling.Reuse,
            TypeNameHandling = TypeNameHandling.All
          });
      }
    }

    public static void CopyValues<T>(T fromObj, ref T toObj, List<PropertyInfo> ignoreProps = null) {
      if (fromObj is null || fromObj.GetType().GetTypeInfo().IsValueType) {
        toObj = fromObj;
        return;
      }

      switch (Type.GetTypeCode(fromObj.GetType())) {
        case TypeCode.Boolean:
        case TypeCode.Byte:
        case TypeCode.DateTime:
        case TypeCode.Decimal:
        case TypeCode.Double:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.SByte:
        case TypeCode.Single:
        case TypeCode.String:
        case TypeCode.UInt16:
        case TypeCode.UInt32:
        case TypeCode.UInt64:
          toObj = fromObj;
          break;
        case var objA when fromObj.GetType().GetInterfaces().Contains(typeof(IList)):
        case var objB when fromObj.GetType().GetInterfaces().Contains(typeof(ICollection)):
        case var objC when fromObj.GetType().GetInterfaces().Contains(typeof(IEnumerable)):
          toObj = (T)Utils.SimpleClone(fromObj, fromObj.GetType());
          break;
        default:
          var props = fromObj.GetType().GetProperties();
          foreach (var prop in props) {
            if (!prop.CanRead || !prop.CanWrite || (ignoreProps is { } && ignoreProps.Contains(prop))) { continue; }
            var fromObjProp = prop.GetValue(fromObj);
            var toObjProp = prop.GetValue(toObj);
            Utils.CopyValues(fromObjProp, ref toObjProp);
            prop.SetValue(toObj, toObjProp);
          }
          break;
      }
    }
  }
}
