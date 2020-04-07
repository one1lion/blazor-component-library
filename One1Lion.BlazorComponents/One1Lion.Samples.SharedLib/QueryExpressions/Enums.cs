using System;
using System.Collections.Generic;
using System.Linq.Expressions;
/// <summary>
/// Defines the available Database Expressions for generating Where Clauses
/// </summary>
namespace One1Lion.Samples.SharedLib.Search.QueryExpressions {
  /// <summary>
  /// The relationship that should be used between statements in a group
  /// </summary>
  public enum RelationshipType {
    And,
    Or,
    AndNot,
    OrNot,
    NotAnd,
    NotOr
  }

  public enum ExpressionType {
    NotSet,
    After,
    Before,
    BeginsWith,
    Between,
    ContainsAll,
    ContainsAny,
    ContainsPhrase,
    DoesNotContainPhrase,
    EndsWith,
    EqualTo,
    FileRepository,
    GreaterThan,
    GreaterThanOrEqualTo,
    InList,
    IsNotNull,
    IsNull,
    LessThan,
    LessThanOrEqualTo,
    Like,
    MissingAll,
    MissingAny,
    NotEqualTo,
    NotInList,
    NotLike,
    OnOrAfter,
    OnOrBefore,
    OtherExtension,
    Default = NotSet
  }

  public class ExpressionTypeFunctions {
    public static Type GetTypeFromExpressionType(ExpressionType expressionType) => expressionType switch
    {
      ExpressionType.After => typeof(DBExpressions.AfterExpression),
      ExpressionType.Before => typeof(DBExpressions.BeforeExpression),
      ExpressionType.BeginsWith => typeof(DBExpressions.BeginsWithExpression),
      ExpressionType.Between => typeof(DBExpressions.BetweenExpression),
      ExpressionType.ContainsAll => typeof(DBExpressions.ContainsAllExpression),
      ExpressionType.ContainsAny => typeof(DBExpressions.ContainsAnyExpression),
      ExpressionType.ContainsPhrase => typeof(DBExpressions.ContainsPhraseExpression),
      ExpressionType.DoesNotContainPhrase => typeof(DBExpressions.DoesNotContainPhraseExpression),
      ExpressionType.EndsWith => typeof(DBExpressions.EndsWithExpression),
      ExpressionType.EqualTo => typeof(DBExpressions.EqualsExpression),
      ExpressionType.GreaterThan => typeof(DBExpressions.GreaterThanExpression),
      ExpressionType.GreaterThanOrEqualTo => typeof(DBExpressions.GreaterThanOrEqualToExpression),
      ExpressionType.InList => typeof(DBExpressions.InListExpression),
      ExpressionType.IsNotNull => typeof(DBExpressions.IsNotNullExpression),
      ExpressionType.IsNull => typeof(DBExpressions.IsNullExpression),
      ExpressionType.LessThan => typeof(DBExpressions.LessThanExpression),
      ExpressionType.LessThanOrEqualTo => typeof(DBExpressions.LessThanOrEqualToExpression),
      ExpressionType.Like => typeof(DBExpressions.LikeExpression),
      ExpressionType.MissingAll => typeof(DBExpressions.MissingAllExpression),
      ExpressionType.MissingAny => typeof(DBExpressions.MissingAnyExpression),
      ExpressionType.NotEqualTo => typeof(DBExpressions.NotEqualsExpression),
      ExpressionType.NotInList => typeof(DBExpressions.NotInListExpression),
      ExpressionType.NotLike => typeof(DBExpressions.NotLikeExpression),
      ExpressionType.OnOrAfter => typeof(DBExpressions.OnOrAfterExpression),
      ExpressionType.OnOrBefore => typeof(DBExpressions.OnOrBeforeExpression),
      _ => typeof(DBExpressions.DBExpressionItem)
    };
  }

  public class ExpressionTypeDisplay {
    public const string NotSet = "{Not Set}";
    public const string After = "is after";
    public const string Before = "is before";
    public const string BeginsWith = "begins with";
    public const string Between = "is between";
    public const string ContainsAll = "contains all of the following";
    public const string ContainsAny = "contains any of the following";
    public const string ContainsPhrase = "contains the phrase";
    public const string DoesNotContainPhrase = "does not contain the phrase";
    public const string EndsWith = "ends with";
    public const string EqualTo = "equals";
    public const string GreaterThan = "is greater than";
    public const string GreaterThanOrEqualTo = "is greater than or equal to";
    public const string InList = "is a value from the following list";
    public const string IsNotNull = "is not null";
    public const string IsNull = "is null";
    public const string LessThan = "is less than";
    public const string LessThanOrEqualTo = "is less than or equal to";
    public const string Like = "matches the pattern";
    public const string MissingAll = "is missing all of the following";
    public const string MissingAny = "is missing any of the following";
    public const string NotEqualTo = "does not equal";
    public const string NotInList = "is not a value from the following list";
    public const string NotLike = "does not match the pattern";
    public const string OnOrAfter = "is on or after";
    public const string OnOrBefore = "is on or before";

    public static string GetDisplayText(ExpressionType expressionType) {
      switch (expressionType) {
        case ExpressionType.NotSet: return NotSet;
        case ExpressionType.After: return After;
        case ExpressionType.Before: return Before;
        case ExpressionType.BeginsWith: return BeginsWith;
        case ExpressionType.Between: return Between;
        case ExpressionType.ContainsAll: return ContainsAll;
        case ExpressionType.ContainsAny: return ContainsAny;
        case ExpressionType.ContainsPhrase: return ContainsPhrase;
        case ExpressionType.DoesNotContainPhrase: return DoesNotContainPhrase;
        case ExpressionType.EndsWith: return EndsWith;
        case ExpressionType.EqualTo: return EqualTo;
        case ExpressionType.GreaterThan: return GreaterThan;
        case ExpressionType.GreaterThanOrEqualTo: return GreaterThanOrEqualTo;
        case ExpressionType.InList: return InList;
        case ExpressionType.IsNotNull: return IsNotNull;
        case ExpressionType.IsNull: return IsNull;
        case ExpressionType.LessThan: return LessThan;
        case ExpressionType.LessThanOrEqualTo: return LessThanOrEqualTo;
        case ExpressionType.Like: return Like;
        case ExpressionType.MissingAll: return MissingAll;
        case ExpressionType.MissingAny: return MissingAny;
        case ExpressionType.NotEqualTo: return NotEqualTo;
        case ExpressionType.NotInList: return NotInList;
        case ExpressionType.NotLike: return NotLike;
        case ExpressionType.OnOrAfter: return OnOrAfter;
        case ExpressionType.OnOrBefore: return OnOrBefore;
      }
      return string.Empty;
    }

    public static List<ExpressionType> GetExpressionTypes(string forType) {
      if (string.IsNullOrWhiteSpace(forType)) { return new List<ExpressionType>() { ExpressionType.NotSet }; }
      return GetExpressionTypes(Type.GetType(forType));
    }

    public static List<ExpressionType> GetExpressionTypes(Type forType) {
      var expressionList = new List<ExpressionType>();
      if (forType is null) {
        return expressionList;
      }
      var isNullable = Nullable.GetUnderlyingType(forType) != null;
      forType = Nullable.GetUnderlyingType(forType) ?? forType;

      TypeCode typeCode;
      try {
        typeCode = Type.GetTypeCode(forType);
      } catch {
        typeCode = TypeCode.Object;
      }
      switch (typeCode) {
        case TypeCode.String:
          expressionList.AddRange(new List<ExpressionType>() {
            ExpressionType.ContainsPhrase,
            ExpressionType.Like,
            ExpressionType.Between,
            ExpressionType.ContainsAll,
            ExpressionType.ContainsAny,
            ExpressionType.BeginsWith,
            ExpressionType.EndsWith,
            ExpressionType.DoesNotContainPhrase,
            ExpressionType.NotLike,
            ExpressionType.MissingAll,
            ExpressionType.MissingAny,
            ExpressionType.EqualTo,
            ExpressionType.NotEqualTo
          });
          break;
        case TypeCode.DateTime:
          expressionList.AddRange(new List<ExpressionType>() {
            ExpressionType.EqualTo,
            ExpressionType.Between,
            ExpressionType.Before,
            ExpressionType.OnOrBefore,
            ExpressionType.After,
            ExpressionType.OnOrAfter,
            ExpressionType.InList,
            ExpressionType.NotInList
          });
          break;
        case TypeCode.Byte:
        case TypeCode.Decimal:
        case TypeCode.Double:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.SByte:
        case TypeCode.Single:
        case TypeCode.UInt16:
        case TypeCode.UInt32:
        case TypeCode.UInt64:
          expressionList.AddRange(new List<ExpressionType>() {
            ExpressionType.EqualTo,
            ExpressionType.Between,
            ExpressionType.LessThan,
            ExpressionType.LessThanOrEqualTo,
            ExpressionType.GreaterThan,
            ExpressionType.GreaterThanOrEqualTo,
            ExpressionType.InList,
            ExpressionType.NotInList
          });
          break;
        case TypeCode.Boolean:
          expressionList.AddRange(new List<ExpressionType>() {
            ExpressionType.EqualTo,
            ExpressionType.NotEqualTo
          });
          break;
        default:
          expressionList.AddRange(new List<ExpressionType>() {
            ExpressionType.EqualTo
          });
          break;
      }
      if (isNullable) { expressionList.AddRange(new List<ExpressionType>() { ExpressionType.IsNull, ExpressionType.IsNotNull }); }
      return expressionList;
    }
  }
}
