using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Between (val1 And val2)
  /// <summary>
  /// Represents the SQL range expression,
  /// e.g. [QueryField] BETWEEN @startValue AND @endValue
  /// </summary>
  public class BetweenExpression : DBExpressions.BetweenExpression, ISQLExpressionItem {
    public BetweenExpression() : base() { }

    public BetweenExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} BETWEEN {(StartValue is null ? "{Enter a start value}" : $"{Surrounder}{StartValue}{Surrounder}")} AND {(EndValue is null ? "{Enter an end value}" : $"{Surrounder}{EndValue}{Surrounder}")}";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} BETWEEN {(StartValue is null ? "{Enter a start value}" : $"@startValue{Order}")} AND {(EndValue is null ? "{Enter an end value}" : $"@endValue{Order}")}";

    public IEnumerable<SqlParameter> SQLParameters() =>
      new List<SqlParameter> {
          new SqlParameter($"startValue{Order}", StartValue),
          new SqlParameter($"endValue{Order}", EndValue)
      };
  }
}
