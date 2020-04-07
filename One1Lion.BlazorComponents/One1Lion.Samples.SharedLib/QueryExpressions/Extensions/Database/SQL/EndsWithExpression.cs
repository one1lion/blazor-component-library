using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Ends With (Like '%phrase')
  /// <summary>
  /// Represents the SQL Like expression,
  /// e.g. [QueryField] Like '%some_value'
  /// </summary>
  public class EndsWithExpression : DBExpressions.EndsWithExpression, ISQLExpressionItem {
    public EndsWithExpression() : base() { }

    public EndsWithExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} LIKE {(Value is null ? "{Enter a value}" : $"'%{Value}'")}";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} {(Value is null ? "{Enter a value}" : $" Like @value{Order}")}";

    public IEnumerable<SqlParameter> SQLParameters() =>
      Value is null ? null : new List<SqlParameter> {
          new SqlParameter($"value{Order}", $"%{Value}")
      };
  }
}
