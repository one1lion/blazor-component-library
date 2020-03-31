using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System.Collections.Generic;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  // Is Not Null
  /// <summary>
  /// Represents the is not null expression,
  /// e.g. [QueryField] is not null
  /// </summary>
  public class IsNotNullExpression : DBExpressionItem {
    public IsNotNullExpression() : base() { }

    public IsNotNullExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public override ExpressionType ExpressionType => ExpressionType.IsNotNull;

    public override int MinimumValueCount => 0;
    public override int MaximumValueCount => 0;

    public override string FormattedDisplay() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : QueryField)} is not null";
  }
}
