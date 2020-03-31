namespace One1Lion.Samples.SharedLib.DbInfo {
  public class LibraryCollection {
    public int Id { get; set; }
    public string DisplayName { get; set; }
    public string Database { get; set; }
    public string FullName { get; set; }
    public int CollectionTableId { get; set; }
    public string NameInDbTypeField { get; set; }
    public string Description { get; set; }
    public bool HasImage { get; set; }
    public virtual CollectionTable CollectionTable { get; set; }
  }
}