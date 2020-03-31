using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System.Collections.Generic;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  // Not Like (Not Like '_expression_with_like_pattern_')
  /// <summary>
  /// Represents the Like expression,
  /// e.g. [QueryField] Not Like '_expression_with_like_pattern_'
  /// </summary>
  public class NotLikeExpression : DBExpressionItem {
    public NotLikeExpression() : base() { }

    public NotLikeExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public override ExpressionType ExpressionType => ExpressionType.NotLike;

    public override int MinimumValueCount => 1;
    public override int MaximumValueCount => 1;

    private protected object Value => Values is null || Values.Count == 0 ? null : Values[0];

    public override string FormattedDisplay() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : QueryField)} does not match the pattern {(Value is null ? "{Enter a value}" : $"'{Value}'")}";
  }
}
