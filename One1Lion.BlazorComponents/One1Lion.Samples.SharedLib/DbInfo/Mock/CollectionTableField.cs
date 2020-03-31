using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.DbInfo {
  public class CollectionTableField {
    public int Id { get; set; }
    public int CollectionTableId { get; set; }
    public int FieldId { get; set; }
    public int FieldTypeId { get; set; }
    public int Precision { get; set; }
    public int MaxLength { get; set; }
    public string DbDefaultValue { get; set; }
    public virtual CollectionTable CollectionTable { get; set; }
    public virtual DbField Field { get; set; }
    public virtual FieldType FieldType { get; set; }
    public virtual CollectionQueryableField QueryableField { get; set; }
  }
}
