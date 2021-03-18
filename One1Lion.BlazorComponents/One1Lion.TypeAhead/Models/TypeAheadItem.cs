namespace One1Lion.TypeAhead {
  public class TypeAheadItem : ITypeAheadItem {
    public string MenuText { get; set; }
    public string Value { get; set; }
    public string DisplayText { get; set; }
    public object Item { get; set; }
  }

  public class TypeAheadItem<TItem> : ITypeAheadItem<TItem> {
    public string MenuText { get; set; }
    public string Value { get; set; }
    public string DisplayText { get; set; }
    public TItem Item { get; set; }
    object ITypeAheadItem.Item {
      get => Item as object;
      set {
        Item = (TItem)value;
      }
    }
  }
}
