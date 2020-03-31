using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Generic {
  public interface IElement {
    string Id { get; }
    bool AndWithNext { get; set; }
    IGroup Parent { get; set; }
  }
}
