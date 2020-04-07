using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.QueryExpressions {
  public class QueryElement : IQueryElement {
    public QueryElement() {
      Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }

    public bool AndWithNext { get; set; } = true;
    public IQueryExpressionGroup Parent { get; set; }
    public virtual string FormattedDisplay() => "QueryElement";

  }
}
