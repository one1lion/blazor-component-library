using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Generic {
  public class Item : Element, IItem {
    public Item() : base() { }
    public string ItemContent { get; set; }
  }
}
