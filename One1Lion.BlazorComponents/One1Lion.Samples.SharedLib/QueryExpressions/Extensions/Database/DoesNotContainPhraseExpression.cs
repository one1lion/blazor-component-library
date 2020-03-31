using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  // Does Not Contain Phrase (Not Like '%phrase%')
  /// <summary>
  /// Represents the Not Like expression,
  /// e.g. [QueryField] Not Like '_expression_with_like_pattern_'
  /// </summary>
  /// <remarks>If there is only one value, this is the same as the LikeExpression</remarks>
  public class DoesNotContainPhraseExpression : DBExpressionItem {
    public DoesNotContainPhraseExpression() : base() { }

    public DoesNotContainPhraseExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public override ExpressionType ExpressionType => ExpressionType.DoesNotContainPhrase;

    public override int MinimumValueCount => 1;
    public override int MaximumValueCount => 1;

    private protected object Value => Values is null || Values.Count == 0 ? null : Values[0];

    public override string FormattedDisplay() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : QueryField)} does not contain the phrase {(Value is null ? "{Enter a value}" : $"'{Value}'")}";
  }
}
