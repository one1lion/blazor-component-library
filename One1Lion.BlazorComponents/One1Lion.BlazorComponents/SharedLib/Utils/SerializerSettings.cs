using Newtonsoft.Json;

namespace One1Lion.BlazorComponents.SharedLib.Utils {
  public static class SerializerSettings {
    public static JsonSerializerSettings NewtonsoftSerializerSettings => new JsonSerializerSettings() {
      ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
      PreserveReferencesHandling = PreserveReferencesHandling.None,
      ObjectCreationHandling = ObjectCreationHandling.Reuse,
      TypeNameHandling = TypeNameHandling.All,
      SerializationBinder = new NetCoreSerializationBinder()
    };
    public static JsonSerializerSettings NewtonsoftDeserializerSettings => new JsonSerializerSettings() {
      ObjectCreationHandling = ObjectCreationHandling.Reuse,
      TypeNameHandling = TypeNameHandling.All,
      SerializationBinder = new NetCoreSerializationBinder()
    };
  }
}
