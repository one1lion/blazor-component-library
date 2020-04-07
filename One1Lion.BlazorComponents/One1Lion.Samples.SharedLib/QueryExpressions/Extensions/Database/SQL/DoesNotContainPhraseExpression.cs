using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Does Not Contain Phrase (Not Like '%phrase%')
  /// <summary>
  /// Represents the SQL Not Like expression,
  /// e.g. [QueryField] Not Like '_expression_with_like_pattern_'
  /// </summary>
  /// <remarks>If there is only one value, this is the same as the LikeExpression</remarks>
  public class DoesNotContainPhraseExpression : DBExpressions.DoesNotContainPhraseExpression, ISQLExpressionItem {
    public DoesNotContainPhraseExpression() : base() { }

    public DoesNotContainPhraseExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} NOT LIKE {(Value is null ? "{Enter a value}" : $"'%{Value}%'")}";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} {(Value is null ? "{Enter a value}" : $" Not Like @value{Order}")}";

    public IEnumerable<SqlParameter> SQLParameters() =>
      Value is null ? null : new List<SqlParameter> {
          new SqlParameter($"value{Order}", $"%{Value}%")
      };
  }
}
