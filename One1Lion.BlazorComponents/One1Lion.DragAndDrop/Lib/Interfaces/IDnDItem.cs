namespace One1Lion.DragAndDrop.Lib {
  public interface IDnDItem : IDnDElement {
    object WrappedItem { get; set; }
  }

  public interface IDnDItem<TItem> : IDnDElement<TItem> {
    TItem WrappedItem { get; set; }
  }
}
