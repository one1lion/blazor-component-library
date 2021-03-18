using System;

namespace One1Lion.DragAndDrop.Lib {
  public class DnDElement : IDnDElement {
    public string Id { get; } = Guid.NewGuid().ToString();
    public string Address { get; protected set; }
    public int IndexInParent { get; set; }

    public IDnDContainer Parent { get; set; }
  }

  public class DnDElement<TItem> : IDnDElement<TItem> {
    public string Id { get; } = Guid.NewGuid().ToString();
    public string Address { get; protected set; }
    public int IndexInParent { get; set; }

    public IDnDContainer<TItem> Parent { get; set; }
  }
}
