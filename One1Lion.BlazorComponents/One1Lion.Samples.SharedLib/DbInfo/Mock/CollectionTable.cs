using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.DbInfo {
  public class CollectionTable {
    public int Id { get; set; }
    public string Database { get; set; }
    public string TableName { get; set; }
    public virtual IEnumerable<LibraryCollection> Collections { get; set; }
    public virtual IEnumerable<CollectionTableField> Fields { get; set; }
  }
}
