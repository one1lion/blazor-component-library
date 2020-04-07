using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  public class DBElement : IDBElement {
    public DBElement() {
      Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }

    public bool AndWithNext { get; set; } = true;
    public IQueryExpressionGroup Parent { get; set; }
    public DBExpressionGroup DBGroupParent => Parent?.GetType()?.GetInterface(nameof(IDBExpressionGroup)) is { } ? (DBExpressionGroup)Parent : null;

    public virtual string FormattedDisplay() => "DBElement";

  }
}
