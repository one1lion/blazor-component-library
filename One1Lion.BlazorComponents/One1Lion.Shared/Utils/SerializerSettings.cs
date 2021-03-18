using Newtonsoft.Json;

namespace One1Lion.Shared {
  public static class SerializerSettings {
    public static JsonSerializerSettings NewtonsoftSerializerSettings(bool prettyPrint = false) => new JsonSerializerSettings() {
      ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
      PreserveReferencesHandling = PreserveReferencesHandling.None,
      ObjectCreationHandling = ObjectCreationHandling.Reuse,
      TypeNameHandling = TypeNameHandling.All,
      SerializationBinder = new NetCoreSerializationBinder(),
      Formatting = prettyPrint ? Formatting.Indented : Formatting.None
    };

    public static JsonSerializerSettings NewtonsoftDeserializerSettings => new JsonSerializerSettings() {
      ObjectCreationHandling = ObjectCreationHandling.Reuse,
      TypeNameHandling = TypeNameHandling.All,
      SerializationBinder = new NetCoreSerializationBinder()
    };
  }
}
