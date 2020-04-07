using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Is Missing Any (NOT (Like '%val1%' And Like '%val2%' And ...))
  /// <summary>
  /// Represents the NOT of an AND combination of SQL like expressions,
  /// e.g. NOT ([QueryField] Like '%val1%' AND [QueryField] Like '%val2%' AND [QueryField] Like ...)
  /// </summary>
  /// <remarks>If there is only one value, this is the same as the LikeExpression</remarks>
  public class MissingAnyExpression : DBExpressions.MissingAnyExpression, ISQLExpressionItem {
    public MissingAnyExpression() : base() { }

    public MissingAnyExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() {
      var sb = new StringBuilder();
      var fld = string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField);
      if (FirstVal is null) {
        sb.Append($"{fld} NOT LIKE {{Add Items}}");
      } else {
        sb.Append($"{fld} LIKE {Surrounder}%{string.Join($"%{Surrounder} AND {fld} LIKE {Surrounder}%", Values)}%{Surrounder}");
        sb.Insert(0, "NOT (").Append(")");
      }
      sb.Insert(0, "(");
      sb.Append(")");
      return sb.ToString();
    }

    public string ParameterizedString() {
      if (string.IsNullOrWhiteSpace(QueryField) || Values is null || Values.Count == 0) { return null; }
      var fld = SQLExpressionFunctions.GetEscapedValue(QueryField);
      var sb = new StringBuilder();
      for (var i = 0; i < Values.Count; i++) {
        if (sb.Length > 0) { sb.Append(" AND "); }
        sb.Append($"{fld} LIKE @value{Order}_{i}");
      }
      sb.Insert(0, "(");
      sb.Append(")");
      return sb.Insert(0, "NOT (").Append(")").ToString();
    }

    public IEnumerable<SqlParameter> SQLParameters() {
      if (Values is null || Values.Count == 0) { return null; }
      var parms = new List<SqlParameter>();
      for (var i = 0; i < Values.Count; i++) {
        parms.Add(new SqlParameter($"value{Order}_{i}", $"%{Values[i]}%"));
      }
      return parms;
    }
  }
}
