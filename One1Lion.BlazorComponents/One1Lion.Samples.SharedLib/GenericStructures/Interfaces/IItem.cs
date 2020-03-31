using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Generic {
  public interface IItem : IElement {
    string ItemContent { get; set; }
  }
}
