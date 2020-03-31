using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Defines the available Database Expressions for generating Where Clauses
/// </summary>
namespace One1Lion.Samples.SharedLib.Search.FileRepositoryExpressions {
  /// <summary>
  /// The interface for any File Repository Expression
  /// </summary>
  public interface IFileRepositoryExpressionItem : IFileRepositoryElement, IQueryExpressionItem {

  }
}
