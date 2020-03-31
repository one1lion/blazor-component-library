using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  /// <summary>
  /// The basic element from which all elements in the DBExpression library are derived
  /// </summary>
  public interface IDBElement : IQueryElement {
    /// <summary>
    /// The <see cref="IDBExpressionGroup" />, if any,
    /// that this element is nested in
    /// </summary>
    [JsonIgnore]
    public IDBExpressionGroup DBGroupParent => (IDBExpressionGroup)Parent;

    /// <summary>
    /// The other elements nested in this 
    /// <see cref="Parent"/>'s element
    /// </summary>
    [JsonIgnore]
    public new IList<IQueryElement> Siblings => DBGroupParent?.Children?.Where(s => s.Id != Id).ToList();
  }
}
