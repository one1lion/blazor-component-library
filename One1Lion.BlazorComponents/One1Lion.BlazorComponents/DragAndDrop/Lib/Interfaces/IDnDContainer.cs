using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.BlazorComponents.DragAndDrop {
  public interface IDnDContainer : IDnDElement {
    List<object> Children { get; }
    object ParentObj { get; }
    List<IDnDElement> ChildElements { get; }
  }

  public interface IDnDContainer<TItem> : IDnDElement<TItem> {
    List<TItem> Children { get; }
    TItem Model { get; }
    List<IDnDElement<TItem>> ChildElements { get; }
  }
}
