using One1Lion.Samples.SharedLib.Search.DBExpressions;
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
    string Name { get; set; }

    /// <summary>
    /// The <see cref="IQueryElement" />s 
    /// nested within this element
    /// </summary>
    IList<IQueryElement> Children { get; set; }

    void AddChild(IQueryElement toAdd, int atIndex = -1);

    IQueryElement NewItem();

    public static IQueryElement NewItem<TParent>(TParent parent) where TParent : IQueryElement, IQueryExpressionGroup {
      return parent is null ? new DBExpressionItem() : parent.NewItem();
    }

    public static IQueryElement NewGroup() {
      throw new NotImplementedException("This method must be defined in the implementing type.");
    }
  }
}
