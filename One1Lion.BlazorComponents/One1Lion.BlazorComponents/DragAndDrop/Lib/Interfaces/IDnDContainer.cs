using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.BlazorComponents.DragAndDrop {
  public interface IDnDContainer : IDnDElement {
    List<object> Children { get; }
    object ParentObj { get; }
  }

  public interface IDnDContainer<TItem> : IDnDElement<TItem> {
    List<TItem> Children { get; }
    TItem ParentObj { get; }
  }
}
