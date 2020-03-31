using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.QueryExpressions {
  public class QueryExpressionItem : QueryElement, IQueryExpressionItem {
    public QueryExpressionItem() : base() { }

    public QueryExpressionItem(ExpressionType expressionType) {
      Id = Guid.NewGuid().ToString();
      ExpressionType = expressionType;
    }

    public QueryExpressionItem(ExpressionType expressionType, string field, List<object> values) {
      Id = Guid.NewGuid().ToString();
      ExpressionType = expressionType;
      QueryField = field;
      Values = values;
    }

    public QueryExpressionItem(string id, ExpressionType expressionType, string field, List<object> values) {
      Id = id;
      ExpressionType = expressionType;
      QueryField = field;
      Values = values;
    }

    public QueryExpressionItem(string id, ExpressionType expressionType, int order, string field, List<object> values) {
      Id = id;
      ExpressionType = expressionType;
      Order = order;
      QueryField = field;
      Values = values;
    }

    public QueryExpressionItem(string id, int order, string field, List<object> vals) {
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

    // TODO: Implement the basic QueryExpressionItem formatted display and Cast
    //public override string FormattedDisplay() {
    //  // TODO: Check if this is a specific expression type and try to cast it
    //  return $"{(!string.IsNullOrWhiteSpace(QueryField) ? QueryField : "{Select a field}")} {(!string.IsNullOrWhiteSpace(OperatorDisplayName) ? OperatorDisplayName : "{operator}")} {(Values == null || Values.Count == 0 ? "{Enter Value(s)}" : string.Join(", ", Values.ToList()))}";
    //}

    //public static T Cast<T>(QueryExpressionItem curExp, bool showDebug = false) { /* TODO: Test to see if  where T : QueryExpressionItem allows for a third descendent and above inheritance */
    //  var runtimeType = Type.GetType("System.RuntimeType");
    //  var baseType = typeof(T) == runtimeType ? typeof(T).BaseType : typeof(T);
    //  if (showDebug) { Console.WriteLine($"Casting {curExp.GetType().FullName} (curExp) to {baseType.FullName} (T)"); }
    //  if (typeof(T) == typeof(QueryExpressionItem)) {
    //    // If we are casting to the QueryExpressionItem type, then use the constructor that sets the ID and ExpressionType
    //    return (T)Activator.CreateInstance(typeof(T), new object[] { curExp.Id, curExp.ExpressionType, curExp.Order, curExp.QueryField, curExp.Values?.ToList() });
    //  } else {
    //    while (baseType != null && baseType?.BaseType != typeof(object) && baseType != typeof(QueryExpressionItem)) { baseType = baseType.BaseType; }
    //    if (baseType != typeof(QueryExpressionItem)) {
    //      throw new InvalidCastException($"The specified type of T ('{typeof(T).FullName}') is not derived from the '{typeof(QueryExpressionItem).FullName}' base class.");
    //    }
    //    // Use the target type's constructor that uses Id, Order, Field, and List of Values
    //    return (T)Activator.CreateInstance(typeof(T), new object[] { curExp.Id, curExp.Order, curExp.QueryField, curExp.Values?.ToList() });
    //  }
    //}
  }
}
