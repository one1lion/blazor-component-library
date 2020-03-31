using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.DbInfo {
  public class DbField {
    public int Id { get; set; }
    public string FieldName { get; set; }
    public int DefaultFieldTypeId { get; set; }
    public string Description { get; set; }
    public virtual ICollection<CollectionTableField> CollectionTableFields { get; set; }
    public virtual FieldType DefaultFieldType { get; set; }
  }
}
