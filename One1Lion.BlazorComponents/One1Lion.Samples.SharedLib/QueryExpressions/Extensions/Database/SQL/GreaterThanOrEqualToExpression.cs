using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Greater Than or Equal To (>=, for non-dates)
  /// <summary>
  /// Represents the SQL greater than or equal to expression,
  /// e.g. [QueryField] >= @value for non-dates
  /// </summary>
  public class GreaterThanOrEqualToExpression : DBExpressions.GreaterThanOrEqualToExpression, ISQLElement {
    public GreaterThanOrEqualToExpression() : base() { }

    public GreaterThanOrEqualToExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} {(Value is null ? " is null" : $" >= {Surrounder}{Value}{Surrounder}")}";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} {(Value is null ? "is null" : $" >= @value{Order}")}";

    public IEnumerable<SqlParameter> SQLParameters() =>
      Value is null ? null : new List<SqlParameter> {
          new SqlParameter($"value{Order}", Value)
      };
  }
}
