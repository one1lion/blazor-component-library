using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Is Not Null
  /// <summary>
  /// Represents the SQL is not null expression,
  /// e.g. [QueryField] is not null
  /// </summary>
  public class IsNotNullExpression : DBExpressions.IsNotNullExpression, ISQLExpressionItem {
    public IsNotNullExpression() : base() { }

    public IsNotNullExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} is not null";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} is not null";

    public IEnumerable<SqlParameter> SQLParameters() => null;
  }
}
