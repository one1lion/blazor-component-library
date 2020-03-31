using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Like (Like '_expression_with_like_pattern_')
  /// <summary>
  /// Represents the SQL Like expression,
  /// e.g. [QueryField] Like '_expression_with_like_pattern_'
  /// </summary>
  public class LikeExpression : DBExpressions.LikeExpression, ISQLElement {
    public LikeExpression() : base() { }

    public LikeExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} Like {(Value is null ? "{Enter a value}" : $"'%{Value}%'")}";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} {(Value is null ? "{Enter a value}" : $" Like @value{Order}")}";

    public IEnumerable<SqlParameter> SQLParameters() =>
      Value is null ? null : new List<SqlParameter> {
          new SqlParameter($"value{Order}", Value)
      };
  }
}
