using System;
using System.Reflection;

namespace One1Lion.BlazorComponents.SharedLib {
  public class Utils {
    public static T SimpleClone<T>(T obj) {
      // If T is one of the known primitive (or primitive-like) types listed below, then simply assign the cloned object to the object to be cloned
      if (obj.GetType().GetTypeInfo().IsValueType) { return obj; }
      switch (Type.GetTypeCode(obj.GetType())) {
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
          var clonedObj = Activator.CreateInstance<T>();
          // Naive approach 1: JsonSerialize the original object and JsonDeserialize back to T
          /*
           * Causes an issue when T is an object with circular references or too deep of a nested structure
          var jsonOptions = new JsonSerializerOptions() {
            MaxDepth = 2,
            IgnoreNullValues = true
          };
          clonedObj = JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(obj, obj.GetType(), jsonOptions), jsonOptions);
          */

          // Naive approach 2: Use reflection to copy readable and writeable properties
          // NOTE: This will copy references when performing the assignment for reference type objects
          // Also, if the object to be cloned has read only fields that are assigned in the constructor, such as Ids, 
          // the cloned object will not be able to set that property
          CopyValues<T>(obj, ref clonedObj);
          return clonedObj;
      }
    }

    public static void CopyValues<T>(T fromObj, ref T toObj) {
      if (fromObj.GetType().GetTypeInfo().IsValueType) { toObj = fromObj; }
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
        default:
          var props = fromObj.GetType().GetProperties();
          foreach (var prop in props) {
            if (!prop.CanRead || !prop.CanWrite) { continue; }
            prop.SetValue(toObj, prop.GetValue(fromObj));
          }
          break;
      }
    }
  }
}
