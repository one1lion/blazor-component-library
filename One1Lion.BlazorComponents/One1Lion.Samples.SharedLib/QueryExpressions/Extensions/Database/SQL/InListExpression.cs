using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // In List (In (comma_separated_list_of_values))
  /// <summary>
  /// Represents the SQL in list expression,
  /// e.g. [QueryField] IN (_comma_separated_list_of_values_)
  /// </summary>
  public class InListExpression : DBExpressions.InListExpression, ISQLElement {
    public InListExpression() : base() { }

    public InListExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} IN ({(FirstVal is null ? "{Add Items}" : $"{Surrounder}{string.Join($"{Surrounder},{Surrounder}", Values)}{Surrounder}")})";

    // Look at https://stackoverflow.com/questions/4502821/how-do-i-translate-a-liststring-into-a-sqlparameter-for-a-sql-in-statement
    //   for an example of how to make the parameter string and subsequent parameters
    // Also shown: https://www.svenbit.com/2014/08/using-sqlparameter-with-sqls-in-clause-in-csharp/
    // This can be used for any of the expressions that use multiple values in a repetative way (that is, not things like BETWEEN or
    //   for things like Contains Any
    public string ParameterizedString() {
      if (Values is null || Values.Count == 0) { return null; }
      var sb = new StringBuilder();
      for (var i = 0; i < Values.Count; i++) {
        if (sb.Length > 0) { sb.Append(","); }
        sb.Append($"@value{Order}_{i}");
      }
      sb.Insert(0, $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} IN (");
      sb.Append(")");
      return sb.ToString();
    }

    public IEnumerable<SqlParameter> SQLParameters() {
      if (Values is null || Values.Count == 0) { return null; }
      var parms = new List<SqlParameter>();
      for (var i = 0; i < Values.Count; i++) {
        parms.Add(new SqlParameter($"value{Order}_{i}", Values[i]));
      }
      return parms;
    }
  }
}
