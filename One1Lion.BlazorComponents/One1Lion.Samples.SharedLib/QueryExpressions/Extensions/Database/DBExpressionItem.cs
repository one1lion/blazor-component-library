using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  public class DBExpressionItem : DBElement, IDBExpressionItem {
    public DBExpressionItem() : base() { }

    public DBExpressionItem(ExpressionType expressionType) {
      Id = Guid.NewGuid().ToString();
      ExpressionType = expressionType;
    }

    public DBExpressionItem(ExpressionType expressionType, string field, List<object> values) {
      Id = Guid.NewGuid().ToString();
      ExpressionType = expressionType;
      QueryField = field;
      Values = values;
    }

    public DBExpressionItem(string id, ExpressionType expressionType, string field, List<object> values) {
      Id = id;
      ExpressionType = expressionType;
      QueryField = field;
      Values = values;
    }

    public DBExpressionItem(string id, ExpressionType expressionType, int order, string field, List<object> values) {
      Id = id;
      ExpressionType = expressionType;
      Order = order;
      QueryField = field;
      Values = values;
    }

    public DBExpressionItem(string id, int order, string field, List<object> vals) {
      Id = id;
      Order = order;
      QueryField = field;
      Values = vals;
    }

    public int Order { get; set; }
    public virtual ExpressionType ExpressionType { get; set; } = ExpressionType.NotSet;
    public virtual string OperatorDisplayName => ExpressionTypeDisplay.GetDisplayText(ExpressionType);
    public string QueryField { get; set; }
    public IList<object> Values { get; set; }

    public virtual int MinimumValueCount => 0;
    public virtual int MaximumValueCount => 0;

    public override string FormattedDisplay() {
      // Check if this is a specific expression type and try to cast it
      if (this.GetType().BaseType != typeof(DBExpressionItem)) {
        switch (ExpressionType) {
          case ExpressionType.After: return (Cast<AfterExpression>(this)).FormattedDisplay();
          case ExpressionType.Before: return (Cast<BeforeExpression>(this)).FormattedDisplay();
          case ExpressionType.BeginsWith: return (Cast<BeginsWithExpression>(this)).FormattedDisplay();
          case ExpressionType.Between: return (Cast<BetweenExpression>(this)).FormattedDisplay();
          case ExpressionType.ContainsAll: return (Cast<ContainsAllExpression>(this)).FormattedDisplay();
          case ExpressionType.ContainsAny: return (Cast<ContainsAnyExpression>(this)).FormattedDisplay();
          case ExpressionType.ContainsPhrase: return (Cast<ContainsPhraseExpression>(this)).FormattedDisplay();
          case ExpressionType.DoesNotContainPhrase: return (Cast<DoesNotContainPhraseExpression>(this)).FormattedDisplay();
          case ExpressionType.EndsWith: return (Cast<EndsWithExpression>(this)).FormattedDisplay();
          case ExpressionType.EqualTo: return (Cast<EqualsExpression>(this)).FormattedDisplay();
          case ExpressionType.GreaterThan: return (Cast<GreaterThanExpression>(this)).FormattedDisplay();
          case ExpressionType.GreaterThanOrEqualTo: return (Cast<GreaterThanOrEqualToExpression>(this)).FormattedDisplay();
          case ExpressionType.InList: return (Cast<InListExpression>(this)).FormattedDisplay();
          case ExpressionType.IsNotNull: return (Cast<IsNotNullExpression>(this)).FormattedDisplay();
          case ExpressionType.IsNull: return (Cast<IsNullExpression>(this)).FormattedDisplay();
          case ExpressionType.LessThan: return (Cast<LessThanExpression>(this)).FormattedDisplay();
          case ExpressionType.LessThanOrEqualTo: return (Cast<LessThanOrEqualToExpression>(this)).FormattedDisplay();
          case ExpressionType.Like: return (Cast<LikeExpression>(this)).FormattedDisplay();
          case ExpressionType.MissingAll: return (Cast<MissingAllExpression>(this)).FormattedDisplay();
          case ExpressionType.MissingAny: return (Cast<MissingAnyExpression>(this)).FormattedDisplay();
          case ExpressionType.NotEqualTo: return (Cast<NotEqualsExpression>(this)).FormattedDisplay();
          case ExpressionType.NotInList: return (Cast<NotInListExpression>(this)).FormattedDisplay();
          case ExpressionType.NotLike: return (Cast<NotLikeExpression>(this)).FormattedDisplay();
          case ExpressionType.OnOrAfter: return (Cast<OnOrAfterExpression>(this)).FormattedDisplay();
          case ExpressionType.OnOrBefore: return (Cast<OnOrBeforeExpression>(this)).FormattedDisplay();
        }
      }
      return $"{(!string.IsNullOrWhiteSpace(QueryField) ? QueryField : "{Select a field}")} {(!string.IsNullOrWhiteSpace(OperatorDisplayName) ? OperatorDisplayName : "{operator}")} {(Values == null || Values.Count == 0 ? "{Enter Value(s)}" : string.Join(", ", Values.ToList()))}";
    }

    public static T Cast<T>(DBExpressionItem curExp, bool showDebug = false) { /* TODO: Test to see if  where T : DBExpression allows for a third descendent and above inheritance */
      var runtimeType = Type.GetType("System.RuntimeType");
      var baseType = typeof(T) == runtimeType ? typeof(T).BaseType : typeof(T);
      if (showDebug) { Console.WriteLine($"Casting {curExp.GetType().FullName} (curExp) to {baseType.FullName} (T)"); }
      if (typeof(T) == typeof(DBExpressionItem)) {
        // If we are casting to the DBExpression type, then use the constructor that sets the ID and ExpressionType
        return (T)Activator.CreateInstance(typeof(T), new object[] { curExp.Id, curExp.ExpressionType, curExp.Order, curExp.QueryField, curExp.Values?.ToList() });
      } else {
        while (baseType != null && baseType?.BaseType != typeof(object) && baseType != typeof(DBExpressionItem)) { baseType = baseType.BaseType; }
        if (baseType != typeof(DBExpressionItem)) {
          throw new InvalidCastException($"The specified type of T ('{typeof(T).FullName}') is not derived from the '{typeof(DBExpressionItem).FullName}' base class.");
        }
        // Use the target type's constructor that uses Id, Order, Field, and List of Values
        return (T)Activator.CreateInstance(typeof(T), new object[] { curExp.Id, curExp.Order, curExp.QueryField, curExp.Values?.ToList() });
      }
    }
  }
}
