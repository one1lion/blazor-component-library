using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  public interface ISQLElement : DBExpressions.IDBElement {
    /// <summary>
    /// Returns the object as a raw SQL Where Clause expression
    /// </summary>
    /// <returns>The raw SQL Where Clause expression this object represents</returns>
    string AsSQLString();

    /// <summary>
    /// Returns the string pattern to use in a parameterized query
    /// </summary>
    /// <returns>The string pattern to use in a parameterized query</returns>
    string ParameterizedString();

    /// <summary>
    /// Returns the SqlParameter(s) to be used in conjunction with the ParameterizedString() function.
    /// </summary>
    /// <returns>The SqlParameter(s) representing the list of Values for this object</returns>
    IEnumerable<SqlParameter> SQLParameters();
  }
}
