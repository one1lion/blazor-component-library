using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Contains Phrase (Like '%phrase%')
  /// <summary>
  /// Represents the SQL Like expression,
  /// e.g. [QueryField] Like '%some_value%'
  /// </summary>
  public class ContainsPhraseExpression : DBExpressions.ContainsPhraseExpression, ISQLElement {
    public ContainsPhraseExpression() : base() { }

    public ContainsPhraseExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} LIKE {(Value is null ? "{Enter a value}" : $"'%{Value}%'")}";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} {(Value is null ? "{Enter a value}" : $" Like @value{Order}")}";

    public IEnumerable<SqlParameter> SQLParameters() =>
      Value is null ? null : new List<SqlParameter> {
          new SqlParameter($"value{Order}", $"%{Value}%")
      };
  }
}
