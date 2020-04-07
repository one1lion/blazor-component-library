using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Is Null
  /// <summary>
  /// Represents the SQL is null expression,
  /// e.g. [QueryField] is null
  /// </summary>
  public class IsNullExpression : DBExpressions.IsNullExpression, ISQLExpressionItem {
    public IsNullExpression() : base() { }

    public IsNullExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} is null";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} is null";

    public IEnumerable<SqlParameter> SQLParameters() => null;
  }
}
