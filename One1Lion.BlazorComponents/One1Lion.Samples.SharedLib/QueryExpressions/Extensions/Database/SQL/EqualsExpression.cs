using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Equals (=)
  /// <summary>
  /// Represents the SQL equals expression,
  /// e.g. [QueryField] = @value
  /// </summary>
  /// <remarks>
  /// If the list of values is null or has no items, this represents the [QueryField] Is Null expression.
  /// It also only uses the first value in the list regardless of how many elements it holds.
  /// </remarks>
  public class EqualsExpression : DBExpressions.EqualsExpression, ISQLExpressionItem {
    public EqualsExpression() : base() { }

    public EqualsExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} {(Value is null ? " is null" : $" = {Surrounder}{Value}{Surrounder}")}";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} {(Value is null ? "is null" : $" = @value{Order}")}";

    public IEnumerable<SqlParameter> SQLParameters() =>
      Value is null ? null : new List<SqlParameter> {
          new SqlParameter($"value{Order}", Value)
      };
  }
}
