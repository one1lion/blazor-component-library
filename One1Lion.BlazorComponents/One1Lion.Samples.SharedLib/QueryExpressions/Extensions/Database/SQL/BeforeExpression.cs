﻿using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  // Before (<, for dates)
  /// <summary>
  /// Represents the SQL less than expression,
  /// e.g. [QueryField] < @value for dates
  /// </summary>
  public class BeforeExpression : DBExpressions.BeforeExpression, ISQLExpressionItem {
    public BeforeExpression() : base() { }

    public BeforeExpression(string id, int order, string field, List<object> vals) : base(id, order, field, vals) { }

    public string AsSQLString() => $"{(string.IsNullOrWhiteSpace(QueryField) ? "{Select a field}" : SQLExpressionFunctions.GetEscapedValue(QueryField))} {(Value is null ? " is null" : $" < {Surrounder}{Value}{Surrounder}")}";

    public string ParameterizedString() => $"{SQLExpressionFunctions.GetEscapedValue(QueryField)} {(Value is null ? "is null" : $" < @value{Order}")}";

    public IEnumerable<SqlParameter> SQLParameters() =>
      Value is null ? null : new List<SqlParameter> {
          new SqlParameter($"value{Order}", Value)
      };
  }
}
