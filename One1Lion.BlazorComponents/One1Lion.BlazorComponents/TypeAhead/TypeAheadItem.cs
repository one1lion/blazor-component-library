using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.BlazorComponents.TypeAhead {
  public class TypeAheadItem {
    public string MenuText { get; set; }
    public string Value { get; set; }
    public string DisplayText { get; set; }
    public object Item { get; set; }
  }

  public class TypeAheadItem<TItem> : TypeAheadItem {
    public new TItem Item { get; set; }
  }
}
