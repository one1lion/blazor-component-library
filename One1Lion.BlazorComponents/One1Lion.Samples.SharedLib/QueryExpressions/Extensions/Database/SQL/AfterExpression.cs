using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // After (>, for dates)
  /// <summary>
  /// Represents the SQL greater than expression,
  /// e.g. [QueryField] > @value for dates
  /// </summary>
  public class AfterExpression : DBExpressions.AfterExpression, ISQLElement {
    public AfterExpression() : base() { }

    public AfterExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} {(Value is null ? " is not null" : $" > {Surrounder}{Value}{Surrounder}")}";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} {(Value is null ? "is not null" : $" > @value{Order}")}";

    public IEnumerable<SqlParameter> SQLParameters() =>
      Value is null ? null : new List<SqlParameter> {
          new SqlParameter($"value{Order}", Value)
      };

    public IEnumerable<SqlParameter> SQLParameters2() =>
      Value is null ? null : new List<SqlParameter>() {
          new SqlParameter($"value{Order}", Value)
      };
  }
}
