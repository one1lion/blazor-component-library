using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace One1Lion.Samples.SharedLib.Search.QueryExpressions {
  /// <summary>
  /// The basic element from which all elements in the QueryExpression library are derived
  /// </summary>
  public interface IQueryElement {
    /// <summary>
    /// A Unique Identifier for this Expression
    /// </summary>
    string Id { get; }
    /// <summary>
    /// Whether or not to AND this expression with the next expression if there is one
    /// </summary>
    bool AndWithNext { get; set; }

    /// <summary>
    /// The <see cref="IQueryExpressionGroup" />, if any,
    /// that this element is nested in
    /// </summary>
    [JsonIgnore]
    IQueryExpressionGroup Parent { get; set; }

    /// <summary>
    /// The other elements nested in this 
    /// <see cref="Parent"/>'s element
    /// </summary>
    [JsonIgnore]
    public IList<IQueryElement> Siblings => Parent?.Children?.Where(s => s.Id != Id).ToList();

    /// <summary>
    /// Generates the string to display when outputting to a User Interface
    /// </summary>
    /// <returns>The string to display when outputting to a User Interface</returns>
    string FormattedDisplay();
  }
}
