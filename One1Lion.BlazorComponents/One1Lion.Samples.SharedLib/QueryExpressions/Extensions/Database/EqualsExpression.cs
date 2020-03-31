using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  // Equals (=)
  /// <summary>
  /// Represents the equals expression,
  /// e.g. [QueryField] = @value
  /// </summary>
  /// <remarks>
  /// If the list of values is null or has no items, this represents the [QueryField] Is Null expression.
  /// It also only uses the first value in the list regardless of how many elements it holds.
  /// </remarks>
  public class EqualsExpression : DBExpressionItem {
    public EqualsExpression() : base() { }

    public EqualsExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public override ExpressionType ExpressionType => ExpressionType.EqualTo;

    public override int MinimumValueCount => 0;
    public override int MaximumValueCount => 1;

    private protected object Value => Values is null || Values.Count == 0 ? null : Values[0];
    private protected string Surrounder => Value is string || Value is DateTime ? "'" : "";

    public override string FormattedDisplay() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : QueryField)} {(Value is null ? " is null" : $" is equal to {Surrounder}{Value}{Surrounder}")}";
  }
}
