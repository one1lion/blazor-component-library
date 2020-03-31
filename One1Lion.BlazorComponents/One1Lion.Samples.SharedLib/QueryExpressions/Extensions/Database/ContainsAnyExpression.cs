using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  // Contains Any (Like '%val1%' Or Like '%val2%' Or ...)
  /// <summary>
  /// Represents an OR combination of like expressions, 
  /// e.g. [QueryField] Like '%val1%' Or [QueryField] Like '%val2%' Or [QueryField] Like ...
  /// </summary>
  /// <remarks>If there is only one value, this is the same as the LikeExpression</remarks>
  public class ContainsAnyExpression : DBExpressionItem {
    public ContainsAnyExpression() : base() { }

    public ContainsAnyExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public override ExpressionType ExpressionType => ExpressionType.ContainsAny;

    public override int MinimumValueCount => 1;
    public override int MaximumValueCount => 50;

    private protected object FirstVal => Values is null || Values.Count == 0 ? null : Values[0];
    private protected string Surrounder => FirstVal is string || FirstVal is DateTime ? "'" : "";

    public override string FormattedDisplay() {
      var sb = new StringBuilder();
      var fld = string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : QueryField;
      if (FirstVal is null) {
        sb.Append($"{fld} contains any of the following: {{Add Items}}");
      } else {
        sb.Append($"{Surrounder}{string.Join($"{Surrounder}, {Surrounder}", Values)}{Surrounder}");
        sb.Insert(0, $"{fld} contains any of the following: ");
      }

      return sb.ToString();
    }
  }
}
