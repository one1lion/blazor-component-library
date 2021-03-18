namespace One1Lion.DragAndDrop.Lib {
  public interface IDnDElement {
    string Id { get; }
    string Address { get; }
    int IndexInParent { get; set; }
    IDnDContainer Parent { get; set; }
  }

  public interface IDnDElement<TElem> {
    string Id { get; }
    string Address { get; }
    int IndexInParent { get; set; }
    IDnDContainer<TElem> Parent { get; set; }
  }
}
