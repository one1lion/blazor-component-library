using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  // Not In List (Not In (comma_separated_list_of_values))
  /// <summary>
  /// Represents the not in list expression,
  /// e.g. [QueryField] NOT IN (_comma_separated_list_of_values_)
  /// </summary>
  public class NotInListExpression : DBExpressionItem {
    public NotInListExpression() : base() { }

    public NotInListExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public override ExpressionType ExpressionType => ExpressionType.NotInList;

    public override int MinimumValueCount => 1;
    public override int MaximumValueCount => 5000;

    private protected object FirstVal => Values is null || Values.Count == 0 ? null : Values[0];
    private protected string Surrounder => FirstVal is string || FirstVal is DateTime ? "'" : "";

    public override string FormattedDisplay() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : QueryField)} is not in the list: {(FirstVal is null ? "{Add Items}" : $"{Surrounder}{string.Join($"{Surrounder},{Surrounder}", Values)}{Surrounder}")}";
  }
}
