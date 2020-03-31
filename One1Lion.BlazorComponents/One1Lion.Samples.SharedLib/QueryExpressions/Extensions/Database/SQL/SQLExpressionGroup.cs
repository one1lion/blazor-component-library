using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  public class SQLExpressionGroup : DBExpressions.DBExpressionGroup, ISQLElement {
    public SQLExpressionGroup() : base() { }

    public new IList<ISQLElement> Children { get; set; }

    public string AsSQLString() {
      if (Children is null || Children.Count == 0) { return string.Empty; }

      var sb = new StringBuilder();
      for (var i = 0; i < Children.Count; i++) {
        var child = Children[i];
        sb.Append(child.AsSQLString());
        if (i < Children.Count - 1) { sb.Append(child.AndWithNext ? " AND " : " OR "); }
      }
      var outStr = sb.ToString();
      while (outStr.EndsWith(" AND ") || outStr.EndsWith(" OR ")) {
        outStr = outStr.Substring(0, outStr.LastIndexOf(outStr.EndsWith(" AND ") ? " AND " : " OR "));
      }

      return !string.IsNullOrWhiteSpace(outStr) ? $"{(NotGroup ? "NOT " : "")}({outStr})" : string.Empty;
    }

    public string ParameterizedString() {
      if (Children is null || Children.Count == 0) { return string.Empty; }

      var sb = new StringBuilder();
      var valCounter = 0;
      for (var i = 0; i < Children.Count; i++) {
        var child = Children[i];
        var childParmString = child.ParameterizedString();

        if (string.IsNullOrWhiteSpace(childParmString)) { continue; }

        if (childParmString.Contains("@")) {
          var theParms = childParmString.Split('@');
          var newChildParamString = new StringBuilder();
          newChildParamString.Append(theParms[0]);

          for (var j = 1; j < theParms.Length; j++) {
            if (string.IsNullOrWhiteSpace(theParms[j])) {
              continue;
            }
            var forRemake = theParms[j].Split(' ').ToList();
            if (forRemake[0].EndsWith(")")) { forRemake.Insert(1, ")"); }
            forRemake.RemoveAt(0);
            newChildParamString.Append($"@p{valCounter++} ");
            newChildParamString.Append(string.Join(' ', forRemake));
          }
          childParmString = newChildParamString.ToString();
        }

        sb.Append(childParmString);
        if (i < Children.Count - 1) { sb.Append(child.AndWithNext ? " AND " : " OR "); }
      }
      var outStr = sb.ToString();
      while (outStr.EndsWith(" AND ") || outStr.EndsWith(" OR ")) {
        outStr = outStr.Substring(0, outStr.LastIndexOf(outStr.EndsWith(" AND ") ? " AND " : " OR "));
      }

      return !string.IsNullOrWhiteSpace(outStr) ? $"{(NotGroup ? "NOT " : "")}({outStr})" : string.Empty;
    }

    public IEnumerable<SqlParameter> SQLParameters() {
      if (Children is null || Children.Count == 0) { return new List<SqlParameter>(); }

      var paramList = new List<SqlParameter>();
      var valCounter = 0;
      for (var i = 0; i < Children.Count; i++) {
        var child = Children[i];
        var childParms = child.SQLParameters() as List<SqlParameter>;
        // Rename the parameters in reverse order to match the renaming in the ParameterizedString method
        for (var j = 0; j < childParms.Count(); j++) {
          childParms[j].ParameterName = $"p{valCounter++}";
        }
        paramList.AddRange(childParms);
      }
      return paramList;
    }
  }
}
