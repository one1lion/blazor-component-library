using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace One1Lion.Samples.SharedLib.DbInfo {
  public class DbFieldViewModel {
    public int CollectionTableFieldId { get; set; }
    public DbField DbField { get; set; }
    public string TypeName { get; set; }
    public TypeCode TypeCode { get; set; }
    public CollectionTable CollectionTable { get; set; }
    public CollectionQueryableField QueryableFieldInfo { get; set; }

    public bool IsQueryable => QueryableFieldInfo != null && QueryableFieldInfo.Id >= 0;

    public static List<DbFieldViewModel> FromDbFieldFlatList(List<DbFieldFlatList> flatList) {
      if (flatList is null) { return null; }
      return flatList.GroupBy(dbf => new {
        dbf.CollectionTableFieldId,
        dbf.DbFieldId,
        dbf.FieldName,
        dbf.FieldType,
        dbf.FieldTypeCode,
        dbf.CollectionTableId,
        dbf.DatabaseName,
        dbf.CollectionTableName,
        dbf.QueryableFieldId,
        dbf.QueryableFieldDisplayName,
        dbf.IsLookup
      }).Select(dbf => new DbFieldViewModel() {
        CollectionTableFieldId = dbf.Key.CollectionTableFieldId,
        DbField = new DbField() {
          Id = dbf.Key.DbFieldId,
          FieldName = dbf.Key.FieldName
        },
        TypeName = dbf.Key.FieldType,
        TypeCode = dbf.Key.FieldTypeCode,
        CollectionTable = new CollectionTable() {
          Id = dbf.Key.CollectionTableId,
          Database = dbf.Key.DatabaseName,
          TableName = dbf.Key.CollectionTableName,
          Collections = dbf.Select(fi => new LibraryCollection() {
            Id = fi.CollectionId,
            NameInDbTypeField = fi.CollectionDbType,
            FullName = fi.CollectionFullName
          })
        },
        QueryableFieldInfo = !dbf.Key.QueryableFieldId.HasValue ? null : new CollectionQueryableField() {
          Id = dbf.Key.QueryableFieldId.Value,
          DisplayName = dbf.Key.QueryableFieldDisplayName,
          IsLookup = dbf.Key.IsLookup
        }
      }).OrderBy(dbf => dbf.DbField.FieldName).ToList();
    }

    public static List<DbFieldFlatList> ToFlatList(List<DbFieldViewModel> dbFields) {
      return DbFieldFlatList.FromDbFieldList(dbFields);
    }

    public static List<DbFieldFlatList> ToFlatList(DbFieldViewModel dbField) {
      return DbFieldFlatList.FromDbFieldList(new List<DbFieldViewModel>() { dbField });
    }

    /// <summary>
    /// Turn this instance of the View Model into a flat list version
    /// </summary>
    /// <returns></returns>
    public List<DbFieldFlatList> ToFlatList() {
      return DbFieldFlatList.FromDbFieldList(new List<DbFieldViewModel>() { this });
    }
  }
}
