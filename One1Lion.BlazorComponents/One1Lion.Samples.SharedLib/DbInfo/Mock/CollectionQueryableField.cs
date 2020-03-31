using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.DbInfo {
  public class CollectionQueryableField {
    public int Id { get; set; }
    public string DisplayName { get; set; }
    public int CollectionTableFieldId { get; set; }
    public bool IsLookup { get; set; }
    public virtual CollectionTableField CollectionTableField { get; set; }
  }
}
