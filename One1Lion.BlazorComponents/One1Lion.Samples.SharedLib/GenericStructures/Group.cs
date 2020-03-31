using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Generic {
  public class Group : Element, IGroup {
    public Group() : base() { }
    public List<IElement> Children { get; set; }

    public void AddChild(IElement toAdd, int atIndex = -1) {
      if (Children is null) { Children = new List<IElement>(); }
      if (atIndex < 0) { atIndex = Children.Count; }
      atIndex = Math.Min(Children.Count, Math.Max(0, atIndex));
      Children.Insert(atIndex, toAdd);
      toAdd.Parent = this;
    }

    public static IElement NewChildItem() {
      return new Item();
    }

    public static IElement NewGroup() {
      return new Group();
    }
  }
}
