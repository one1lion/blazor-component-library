using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.FileRepositoryExpressions {
  /// <summary>
  /// Extends the basic <see cref="IFileRepositoryElement"/>
  /// by adding a <see cref="Children"/>
  /// property, enabling this element to contain child 
  /// <see cref="FileRepositoryElement"/>s
  /// </summary>
  public interface IFileRepositoryExpressionGroup : IFileRepositoryElement, IQueryExpressionGroup {
    void AddChild(FileRepositoryElement toAdd, int atIndex = -1);
  }
}
