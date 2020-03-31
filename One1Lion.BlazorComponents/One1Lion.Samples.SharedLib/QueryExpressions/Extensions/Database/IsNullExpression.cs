using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System.Collections.Generic;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  // Is Null
  /// <summary>
  /// Represents the is null expression,
  /// e.g. [QueryField] is null
  /// </summary>
  public class IsNullExpression : DBExpressionItem {
    public IsNullExpression() : base() { }

    public IsNullExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public override ExpressionType ExpressionType => ExpressionType.IsNull;

    public override int MinimumValueCount => 0;
    public override int MaximumValueCount => 0;

    public override string FormattedDisplay() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : QueryField)} is null";
  }
}
