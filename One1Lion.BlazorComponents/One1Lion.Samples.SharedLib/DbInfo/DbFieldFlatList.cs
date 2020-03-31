using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace One1Lion.Samples.SharedLib.DbInfo {
  public class DbFieldFlatList {
    public DbFieldFlatList OriginalValues { get; set; }
    public int CollectionTableFieldId { get; set; }
    public int DbFieldId { get; set; }
    public string FieldName { get; set; }
    public string FieldType { get; set; }
    public TypeCode FieldTypeCode { get; set; }
    public int CollectionTableId { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionTableName { get; set; }
    public int CollectionId { get; set; }
    public string CollectionFullName { get; set; }
    public string CollectionName { get; set; }
    public string CollectionDbType { get; set; }
    public int? QueryableFieldId { get; set; }
    public string QueryableFieldDisplayName { get; set; }
    public bool IsQueryable => QueryableFieldId.HasValue && QueryableFieldId.Value >= 0;
    public bool IsLookup { get; set; }
    public bool HasChanges => FieldsWithChanges.Any();

    private List<string> fieldsWithChanges = new List<string>();
    public List<string> FieldsWithChanges {
      get {
        fieldsWithChanges.Clear();
        if (CollectionTableFieldId != (OriginalValues?.CollectionTableFieldId ?? -5)) { fieldsWithChanges.Add(nameof(CollectionTableFieldId)); }
        if (DbFieldId != (OriginalValues?.DbFieldId ?? -5)) { fieldsWithChanges.Add(nameof(DbFieldId)); }
        if (FieldName != OriginalValues?.FieldName) { fieldsWithChanges.Add(nameof(FieldName)); }
        if (FieldType != OriginalValues?.FieldType) { fieldsWithChanges.Add(nameof(FieldType)); }
        if (FieldTypeCode != OriginalValues?.FieldTypeCode) { fieldsWithChanges.Add(nameof(FieldTypeCode)); }
        if (CollectionTableId != (OriginalValues?.CollectionTableId ?? -5)) { fieldsWithChanges.Add(nameof(CollectionTableId)); }
        if (DatabaseName != OriginalValues?.DatabaseName) { fieldsWithChanges.Add(nameof(DatabaseName)); }
        if (CollectionTableName != OriginalValues?.CollectionTableName) { fieldsWithChanges.Add(nameof(CollectionTableName)); }
        if (CollectionId != (OriginalValues?.CollectionId ?? -5)) { fieldsWithChanges.Add(nameof(CollectionId)); }
        if (CollectionFullName != OriginalValues?.CollectionFullName) { fieldsWithChanges.Add(nameof(CollectionFullName)); }
        if (CollectionName != OriginalValues?.CollectionName) { fieldsWithChanges.Add(nameof(CollectionName)); }
        if (CollectionDbType != OriginalValues?.CollectionDbType) { fieldsWithChanges.Add(nameof(CollectionDbType)); }
        if (QueryableFieldId != OriginalValues?.QueryableFieldId) { fieldsWithChanges.Add(nameof(QueryableFieldId)); }
        if (QueryableFieldDisplayName != OriginalValues?.QueryableFieldDisplayName) { fieldsWithChanges.Add(nameof(QueryableFieldDisplayName)); }
        if (IsLookup != OriginalValues?.IsLookup) { fieldsWithChanges.Add(nameof(IsLookup)); }
        return fieldsWithChanges;
      }
    }

    public static List<DbFieldFlatList> FromDbFieldList(List<DbFieldViewModel> dbFields) {
      if (dbFields is null) { return null; }
      if (dbFields.Count() == 0) { return new List<DbFieldFlatList>(); }
      var retList = new List<DbFieldFlatList>();
      foreach (var dbf in dbFields) {
        retList.AddRange(dbf.CollectionTable.Collections.Select(c => new DbFieldFlatList() {
          CollectionTableFieldId = dbf.CollectionTableFieldId,
          DbFieldId = dbf.DbField.Id,
          FieldName = dbf.DbField.FieldName,
          FieldType = dbf.TypeName,
          FieldTypeCode = dbf.TypeCode,
          CollectionTableId = c.CollectionTableId,
          DatabaseName = dbf.CollectionTable.Database,
          CollectionTableName = dbf.CollectionTable.TableName,
          CollectionId = c.Id,
          CollectionFullName = c.FullName,
          CollectionName = c.DisplayName,
          CollectionDbType = c.NameInDbTypeField,
          QueryableFieldId = dbf.QueryableFieldInfo == null ? default : dbf.QueryableFieldInfo.Id,
          QueryableFieldDisplayName = dbf.QueryableFieldInfo == null ? null : dbf.QueryableFieldInfo.DisplayName,
          IsLookup = dbf.QueryableFieldInfo == null ? false : dbf.QueryableFieldInfo.IsLookup
        }).ToList());
      }
      retList.ForEach(ov => ov.OriginalValues = (DbFieldFlatList)ov.MemberwiseClone());
      return retList;
    }

    public static List<DbFieldViewModel> ToViewModel(List<DbFieldFlatList> flatList) {
      return DbFieldViewModel.FromDbFieldFlatList(flatList);
    }

    public void ApplyChanges() {
      OriginalValues.DbFieldId = DbFieldId;
      OriginalValues.FieldName = FieldName;
      OriginalValues.FieldType = FieldType;
      OriginalValues.FieldTypeCode = FieldTypeCode;
      OriginalValues.DatabaseName = DatabaseName;
      OriginalValues.CollectionTableId = CollectionTableId;
      OriginalValues.CollectionTableName = CollectionTableName;
      OriginalValues.CollectionId = CollectionId;
      OriginalValues.CollectionName = CollectionName;
      OriginalValues.CollectionDbType = CollectionDbType;
      OriginalValues.CollectionFullName = CollectionFullName;
      OriginalValues.QueryableFieldId = QueryableFieldId;
      OriginalValues.QueryableFieldDisplayName = QueryableFieldDisplayName;
      OriginalValues.IsLookup = IsLookup;
    }

    public void RestoreOrignalValues() {
      DbFieldId = OriginalValues.DbFieldId;
      FieldName = OriginalValues.FieldName;
      FieldType = OriginalValues.FieldType;
      FieldTypeCode = OriginalValues.FieldTypeCode;
      DatabaseName = OriginalValues.DatabaseName;
      CollectionTableId = OriginalValues.CollectionTableId;
      CollectionTableName = OriginalValues.CollectionTableName;
      CollectionId = OriginalValues.CollectionId;
      CollectionName = OriginalValues.CollectionName;
      CollectionDbType = OriginalValues.CollectionDbType;
      CollectionFullName = OriginalValues.CollectionFullName;
      QueryableFieldId = OriginalValues.QueryableFieldId;
      QueryableFieldDisplayName = OriginalValues.QueryableFieldDisplayName;
      IsLookup = OriginalValues.IsLookup;
    }
  }
}