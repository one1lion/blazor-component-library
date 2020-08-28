using System;

namespace One1Lion.BlazorComponents.DragAndDrop {
  public class DnDItemWrapper : IDnDItem {
    public string Id { get; } = Guid.NewGuid().ToString();
    public object WrappedItem { get; set; }
    public string Address { get; protected set; }
    public int IndexInParent { get; set; }

    public IDnDContainer Parent { get; set; }
  }

  public class DnDItemWrapper<TItem> : IDnDItem<TItem> {
    public string Id { get; } = Guid.NewGuid().ToString();
    public TItem WrappedItem { get; set; }
    public string Address { get; protected set; }
    public int IndexInParent { get; set; }

    public IDnDContainer<TItem> Parent { get; set; }
  }
}
