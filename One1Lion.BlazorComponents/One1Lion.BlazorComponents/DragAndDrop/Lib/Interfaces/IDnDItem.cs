namespace One1Lion.BlazorComponents.DragAndDrop {
  public interface IDnDItem : IDnDElement {
    object WrappedItem { get; set; }
  }

  public interface IDnDItem<TItem> : IDnDElement<TItem> {
    TItem WrappedItem { get; set; }
  }
}
