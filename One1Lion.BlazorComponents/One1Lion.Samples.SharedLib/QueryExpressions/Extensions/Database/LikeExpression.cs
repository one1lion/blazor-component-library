using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System.Collections.Generic;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  // Like (Like '_expression_with_like_pattern_')
  /// <summary>
  /// Represents the Like expression,
  /// e.g. [QueryField] Like '_expression_with_like_pattern_'
  /// </summary>
  public class LikeExpression : DBExpressionItem {
    public LikeExpression() : base() { }

    public LikeExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public override ExpressionType ExpressionType => ExpressionType.Like;

    public override int MinimumValueCount => 1;
    public override int MaximumValueCount => 1;

    private protected object Value => Values is null || Values.Count == 0 ? null : Values[0];

    public override string FormattedDisplay() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : QueryField)} matches the pattern {(Value is null ? "{Enter a value}" : $"'{Value}'")}";
  }
}
