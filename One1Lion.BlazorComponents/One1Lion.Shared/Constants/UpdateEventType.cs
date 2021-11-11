using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Shared {
  public enum UpdateEventType {
    NotSet,
    ItemAdded,
    ItemEdited,
    ItemCopied,
    ItemMoved,
    ItemsGrouped,
    ItemRemoved,
    Ungrouped
  }
}
