using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Generic {
  public interface IGroup : IElement {
    List<IElement> Children { get; set; }

    void AddChild(IElement toAdd, int atIndex = -1);
    static IElement NewChildItem() {
      throw new NotImplementedException("This method must be defined in the Implementing Class.");
    }
    static IElement NewGroup() {
      throw new NotImplementedException("This method must be defined in the Implementing Class.");
    }
  }
}
