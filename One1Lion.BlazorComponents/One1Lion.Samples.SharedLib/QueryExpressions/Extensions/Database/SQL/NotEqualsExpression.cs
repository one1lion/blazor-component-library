using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Does Not Equal (<>)
  /// <summary>
  /// Represents the SQL not equals expression,
  /// e.g. [QueryField] <> @value
  /// </summary>
  /// <remarks>
  /// If the list of values is null or has no items, this represents the [QueryField] Is Not Null expression.
  /// It also only uses the first value in the list regardless of how many elements it holds.
  /// </remarks>
  public class NotEqualsExpression : DBExpressions.NotEqualsExpression, ISQLExpressionItem {
    public NotEqualsExpression() : base() { }

    public NotEqualsExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} {(Value is null ? " is not null" : $" <> {Surrounder}{Value}{Surrounder}")}";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} {(Value is null ? "is not null" : $" <> @value{Order}")}";

    public IEnumerable<SqlParameter> SQLParameters() =>
      Value is null ? null : new List<SqlParameter> {
          new SqlParameter($"value{Order}", Value)
      };
  }
}
