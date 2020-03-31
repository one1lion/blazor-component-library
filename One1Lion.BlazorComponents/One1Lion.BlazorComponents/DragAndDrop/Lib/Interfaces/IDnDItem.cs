using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.BlazorComponents.DragAndDrop {
  public interface IDnDItem : IDnDElement {
    object WrappedItem { get; set; }
  }

  public interface IDnDItem<TItem> : IDnDElement<TItem> {
    TItem WrappedItem { get; set; }
  }

}
