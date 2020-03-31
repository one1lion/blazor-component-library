using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Not Like (Not Like '_expression_with_like_pattern_')
  /// <summary>
  /// Represents the SQL Like expression,
  /// e.g. [QueryField] Not Like '_expression_with_like_pattern_'
  /// </summary>
  public class NotLikeExpression : DBExpressions.NotLikeExpression, ISQLElement {
    public NotLikeExpression() : base() { }

    public NotLikeExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} NOT LIKE {(Value is null ? "{Enter a value}" : $"'{Value}'")}";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} {(Value is null ? "{Enter a value}" : $" NOT LIKE @value{Order}")}";

    public IEnumerable<SqlParameter> SQLParameters() =>
      Value is null ? null : new List<SqlParameter> {
        new SqlParameter($"value{Order}", Value)
      };
  }
}
