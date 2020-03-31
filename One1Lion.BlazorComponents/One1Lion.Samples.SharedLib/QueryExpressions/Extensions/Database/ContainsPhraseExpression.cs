using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System.Collections.Generic;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  // Contains Phrase (Like '%phrase%')
  /// <summary>
  /// Represents the Like expression,
  /// e.g. [QueryField] Like '%some_value%'
  /// </summary>
  public class ContainsPhraseExpression : DBExpressionItem {
    public ContainsPhraseExpression() : base() { }

    public ContainsPhraseExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public ContainsPhraseExpression(string field, string phrase) : base() {
      QueryField = field;
      Values = string.IsNullOrWhiteSpace(phrase) ? null : new List<object>() { phrase };
    }

    public override ExpressionType ExpressionType => ExpressionType.ContainsPhrase;

    public override int MinimumValueCount => 1;
    public override int MaximumValueCount => 1;

    private protected object Value => Values is null || Values.Count == 0 ? null : Values[0];

    public override string FormattedDisplay() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : QueryField)} contains the phrase {(Value is null ? "{Enter a value}" : $"'{Value}'")}";
  }
}
