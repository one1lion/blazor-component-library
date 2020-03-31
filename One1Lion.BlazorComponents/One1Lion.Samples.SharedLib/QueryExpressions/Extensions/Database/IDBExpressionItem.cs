using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// Defines the available Database Expressions for generating Where Clauses
/// </summary>
namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  /// <summary>
  /// The interface for any Database Expression
  /// </summary>
  public interface IDBExpressionItem : IDBElement, IQueryExpressionItem {
    /// <summary>
    /// The text to display in the User Interface
    /// </summary>
    string OperatorDisplayName { get; }
    /// <summary>
    /// The field being evaluated
    /// </summary>
    string QueryField { get; set; }
    /// <summary>
    /// The <see cref="ExpressionType"/> for this Expression Item
    /// </summary>
    ExpressionType ExpressionType { get; set; }
    /// <summary>
    /// The list of values to use in the evaluation
    /// </summary>
    IList<object> Values { get; set; }

    /// <summary>
    /// The minimum number of Values for this to be a valid Expression
    /// </summary>
    int MinimumValueCount { get; }
    /// <summary>
    /// The maximum number of Values for this to be a valid Expression
    /// </summary>
    int MaximumValueCount { get; }
  }
}
