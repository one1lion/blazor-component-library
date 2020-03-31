using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.FileRepositoryExpressions {
  /// <summary>
  /// The basic element from which all elements in the FileRepositoryExpression library are derived
  /// </summary>
  public interface IFileRepositoryElement : IQueryElement {
    /// <summary>
    /// The <see cref="IFileRepositoryExpressionGroup" />, if any,
    /// that this element is nested in
    /// </summary>
    [JsonIgnore]
    public IFileRepositoryExpressionGroup FileRepositoryGroupParent => (IFileRepositoryExpressionGroup)Parent;

    /// <summary>
    /// The other elements nested in this 
    /// <see cref="Parent"/>'s element
    /// </summary>
    [JsonIgnore]
    public new IList<IQueryElement> Siblings => FileRepositoryGroupParent?.Children?.Where(s => s.Id != Id).ToList();
  }
}
