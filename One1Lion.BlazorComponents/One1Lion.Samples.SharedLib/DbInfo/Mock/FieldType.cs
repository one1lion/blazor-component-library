using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.DbInfo {
  public class FieldType {
    public int Id { get; set; }
    public string DbTypeName { get; set; }
    public string FriendlyName { get; set; }
    public string CSharpTypeName { get; set; }
    public TypeCode CSharpTypeCode { get; set; }
    public virtual ICollection<CollectionTableField> CollectionTableFields { get; set; }
    public virtual ICollection<DbField> DbFields { get; set; }
  }
}
