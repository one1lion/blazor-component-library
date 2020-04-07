using One1Lion.Samples.SharedLib.Search.DBExpressions;
using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

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

    /// <summary>
    /// Returns a SQLExpressionGroup from a supplied DBExpressionGroup
    /// </summary>
    /// <param name="dbGroup">The DBExpressionGroup to return as a SQLExpressionGroup</param>
    /// <returns>A SQLExpressionGroup from a DBExpressionGroup</returns>
    public static SQLExpressionGroup FromDBGroup(IQueryExpressionGroup dbGroup) {
      var newGroup = new SQLExpressionGroup() {
        Id = dbGroup.Id,
        Name = dbGroup.Name,
        Parent = dbGroup.Parent,
        NotGroup = dbGroup.NotGroup,
        AndWithNext = dbGroup.AndWithNext
      };
      if ((dbGroup.Children?.Count ?? 0) == 0) {
        return newGroup;
      } else {
        newGroup.Children = new List<ISQLElement>();
      }
      foreach (var curChild in dbGroup.Children) {
        if (curChild.GetType().GetInterface(nameof(IDBExpressionGroup)) is { }) {
          var subGroup = SQLExpressionFunctions.FromDBGroup(curChild as IDBExpressionGroup);
          subGroup.Parent = newGroup;
          newGroup.Children.Add(subGroup);
        } else {
          var newChild = SQLExpressionFunctions.FromDBItem(curChild as IDBExpressionItem);
          newChild.Parent = newGroup;
          newGroup.Children.Add(newChild);
        }
      }
      return newGroup;
    }

    /// <summary>
    /// Returns a SQLExpressionItem from a supplied DBExpressionItem
    /// </summary>
    /// <param name="dbItem">The DBExpressionItem to return as a SQLExpressionItem</param>
    /// <returns>A SQLExpressionItem from a DBExpressionItem</returns>
    /// <exception cref="InvalidCastException">Thrown when the supplied argument (<paramref name="dbItem"/>) is not a specific extension of DBExpressionItem</exception>
    /// <exception cref="NotImplementedException">Thrown when the supplied argument (<paramref name="dbItem"/>) has an ExpressionType that has not been implemented in SQLExpressions</exception>
    private static ISQLElement FromDBItem(IDBExpressionItem dbItem) {
      var itemType = ExpressionTypeFunctions.GetTypeFromExpressionType(dbItem.ExpressionType);
      if (itemType == typeof(DBExpressionItem)) {
        throw new InvalidCastException($"Expected a specific extension of {typeof(DBExpressionItem).FullName}. Found {dbItem.GetType().FullName}.");
      }
      ISQLExpressionItem sqlItem = itemType switch
      {
        var it when it == typeof(DBExpressions.AfterExpression) => new AfterExpression(),
        var it when it == typeof(DBExpressions.BeforeExpression) => new BeforeExpression(),
        var it when it == typeof(DBExpressions.BeginsWithExpression) => new BeginsWithExpression(),
        var it when it == typeof(DBExpressions.BetweenExpression) => new BetweenExpression(),
        var it when it == typeof(DBExpressions.ContainsAllExpression) => new ContainsAllExpression(),
        var it when it == typeof(DBExpressions.ContainsAnyExpression) => new ContainsAnyExpression(),
        var it when it == typeof(DBExpressions.ContainsPhraseExpression) => new ContainsPhraseExpression(),
        var it when it == typeof(DBExpressions.DoesNotContainPhraseExpression) => new DoesNotContainPhraseExpression(),
        var it when it == typeof(DBExpressions.EndsWithExpression) => new EndsWithExpression(),
        var it when it == typeof(DBExpressions.EqualsExpression) => new EqualsExpression(),
        var it when it == typeof(DBExpressions.GreaterThanExpression) => new GreaterThanExpression(),
        var it when it == typeof(DBExpressions.GreaterThanOrEqualToExpression) => new GreaterThanOrEqualToExpression(),
        var it when it == typeof(DBExpressions.InListExpression) => new InListExpression(),
        var it when it == typeof(DBExpressions.IsNotNullExpression) => new IsNotNullExpression(),
        var it when it == typeof(DBExpressions.IsNullExpression) => new IsNullExpression(),
        var it when it == typeof(DBExpressions.LessThanExpression) => new LessThanExpression(),
        var it when it == typeof(DBExpressions.LessThanOrEqualToExpression) => new LessThanOrEqualToExpression(),
        var it when it == typeof(DBExpressions.LikeExpression) => new LikeExpression(),
        var it when it == typeof(DBExpressions.MissingAllExpression) => new MissingAllExpression(),
        var it when it == typeof(DBExpressions.MissingAnyExpression) => new MissingAnyExpression(),
        var it when it == typeof(DBExpressions.NotEqualsExpression) => new NotEqualsExpression(),
        var it when it == typeof(DBExpressions.NotInListExpression) => new NotInListExpression(),
        var it when it == typeof(DBExpressions.NotLikeExpression) => new NotLikeExpression(),
        var it when it == typeof(DBExpressions.OnOrAfterExpression) => new OnOrAfterExpression(),
        var it when it == typeof(DBExpressions.OnOrBeforeExpression) => new OnOrBeforeExpression(),
        _ => throw new NotImplementedException($"The DBExpressionType {itemType.FullName} does not have a matching SQLElement implementation.")
      };
      sqlItem.Id = dbItem.Id;
      sqlItem.AndWithNext = dbItem.AndWithNext;
      sqlItem.ExpressionType = dbItem.ExpressionType;
      sqlItem.QueryField = dbItem.QueryField;
      sqlItem.Values = dbItem.Values;

      return sqlItem;
    }
  }
}
