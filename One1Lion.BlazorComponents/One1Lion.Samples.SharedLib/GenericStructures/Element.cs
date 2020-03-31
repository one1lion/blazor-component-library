using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Generic {
  public class Element : IElement {
    public Element() {
      Id = Guid.NewGuid().ToString();
    }

    public bool AndWithNext { get; set; } = true;

    public string Id { get; private protected set; }
    public IGroup Parent { get; set; }

  }
}
