using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Generic {
  public interface IGroup : IElement {
    List<IElement> Children { get; set; }

    void AddChild(IElement toAdd, int atIndex = -1);

    IElement NewChildItem();

    static IElement NewChildItem<TParent>(TParent parent) where TParent : IElement, IGroup {
      return parent.NewChildItem();
    }
    static IElement NewGroup() {
      throw new NotImplementedException("This method must be defined in the Implementing Class.");
    }
  }
}
