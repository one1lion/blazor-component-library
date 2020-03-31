using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  // Begins With (Like 'phrase%')
  /// <summary>
  /// Represents the Like expression using the entered value with the wildcard (%) token at the end,
  /// e.g. [QueryField] Like 'some_value%'
  /// </summary>
  public class BeginsWithExpression : DBExpressionItem {
    public BeginsWithExpression() : base() { }

    public BeginsWithExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public override ExpressionType ExpressionType => ExpressionType.BeginsWith;

    public override int MinimumValueCount => 1;
    public override int MaximumValueCount => 1;

    private protected object Value => Values is null || Values.Count == 0 ? null : Values[0];

    public override string FormattedDisplay() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : QueryField)} {(Value is null ? " {Enter a value}" : $" begins with '{Value}'")}";
  }
}
