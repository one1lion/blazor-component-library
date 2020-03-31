using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  /// <summary>
  /// Extends the basic <see cref="IDBElement"/>
  /// by adding a <see cref="Children"/>
  /// property, enabling this element to contain child 
  /// <see cref="DBElement"/>s
  /// </summary>
  public interface IDBExpressionGroup : IDBElement, IQueryExpressionGroup {
    void AddChild(DBElement toAdd, int atIndex = -1);
  }
}
