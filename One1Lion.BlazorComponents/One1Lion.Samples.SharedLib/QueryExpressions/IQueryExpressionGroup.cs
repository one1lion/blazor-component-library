using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.QueryExpressions {
  /// <summary>
  /// Extends the basic <see cref="IQueryElement"/>
  /// by adding a <see cref="Children"/>
  /// property, enabling this element to contain child 
  /// <see cref="IQueryElement"/>s
  /// </summary>
  public interface IQueryExpressionGroup : IQueryElement {
    /// <summary>
    /// Whether or not this group should be "Not"ed
    /// </summary>
    bool NotGroup { get; set; }

    /// <summary>
    /// The <see cref="IQueryElement" />s 
    /// nested within this element
    /// </summary>
    List<IQueryElement> Children { get; set; }

    void AddChild(IQueryElement toAdd, int atIndex = -1);

    public static IQueryElement NewItem() {
      throw new NotImplementedException("This method must be defined in the implementing class.");
    }
    public static IQueryElement NewGroup() {
      throw new NotImplementedException("This method must be defined in the implementing class.");
    }
  }
}
