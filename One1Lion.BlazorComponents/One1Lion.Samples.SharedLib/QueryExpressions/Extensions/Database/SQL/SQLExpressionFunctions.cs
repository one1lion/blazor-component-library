using Microsoft.Data.SqlClient;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  public class SQLExpressionFunctions {
    // TODO: Consider using EntitySQL
    // https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/language-reference/entity-sql-language?redirectedfrom=MSDN
    /// <summary>
    /// Uses SqlCommandBuilder to generate a properly escaped value.  This can be used for table and field names when 
    /// generating SQL
    /// </summary>
    /// <param name="forText">The text to apply the QuiteIdentifier to</param>
    /// <returns></returns>
    public static string GetEscapedValue(string forText) {
      using (var builder = new SqlCommandBuilder()) {
        var escapedValue = builder.QuoteIdentifier(forText);
        return escapedValue;
      }
    }
  }
}
