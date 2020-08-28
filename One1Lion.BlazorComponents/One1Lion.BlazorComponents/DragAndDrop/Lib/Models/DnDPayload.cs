namespace One1Lion.BlazorComponents.DragAndDrop {
  public class DnDPayload<TItem> {
    public IDnDContainer<TItem> Parent { get; set; }
    public int IndexInParent { get; set; }
    public IDnDElement<TItem> WrappingElement { get; set; }
    public bool GroupAsFirst { get; set; }
    public TItem Item => IndexInParent < 0 || Parent?.Children is null ? default : Parent.Children[IndexInParent];

    public bool IsContainer => WrappingElement.GetType().GetInterface(typeof(IDnDContainer<TItem>).Name) is { };
  }
}
