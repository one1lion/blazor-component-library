using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  // Between (val1 And val2)
  /// <summary>
  /// Represents the range expression,
  /// e.g. [QueryField] BETWEEN @startValue AND @endValue
  /// </summary>
  public class BetweenExpression : DBExpressionItem {
    public BetweenExpression() : base() { }

    public BetweenExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public override ExpressionType ExpressionType => ExpressionType.Between;

    public override int MinimumValueCount => 2;
    public override int MaximumValueCount => 2;

    private protected object StartValue => Values is null || Values.Count == 0 ? null : Values[0];
    private protected object EndValue => Values is null || Values.Count <= 1 ? null : Values[1];
    private protected string Surrounder => StartValue is string || StartValue is DateTime ? "'" : "";

    public override string FormattedDisplay() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : QueryField)} is between {(StartValue is null ? "{Enter a start value}" : $"{Surrounder}{StartValue}{Surrounder}")} and {(EndValue is null ? "{Enter an ending value}" : $"{Surrounder}{EndValue}{Surrounder}")}";
  }
}
