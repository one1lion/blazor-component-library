using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  // On or After (>=, for dates)
  /// <summary>
  /// Represents the greater than or equal to expression,
  /// e.g. [QueryField] >= @value for dates
  /// </summary>
  public class OnOrAfterExpression : DBExpressionItem {
    public OnOrAfterExpression() : base() { }

    public OnOrAfterExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public override ExpressionType ExpressionType => ExpressionType.OnOrAfter;

    public override int MinimumValueCount => 0;
    public override int MaximumValueCount => 1;

    private protected object Value => Values is null || Values.Count == 0 ? null : Values[0];
    private protected string Surrounder => Value is string || Value is DateTime ? "'" : "";

    public override string FormattedDisplay() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : QueryField)} {(Value is null ? " is null" : $" is on or after {Surrounder}{Value}{Surrounder}")}";
  }
}
