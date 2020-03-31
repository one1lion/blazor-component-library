using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Defines the available Expressions for generating queries
/// </summary>
namespace One1Lion.Samples.SharedLib.Search.QueryExpressions {
  // TODO: Use groups (List<SQLExpressionCollection> ExpressionGroups) for grouping
  // together SQLExpressions with a relationship type

  /// <summary>
  /// The interface for any Query Expression
  /// </summary>
  public interface IQueryExpressionItem : IQueryElement {
    
  }
}
