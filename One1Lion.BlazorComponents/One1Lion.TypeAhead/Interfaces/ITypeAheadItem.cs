namespace One1Lion.TypeAhead {
  public interface ITypeAheadItem {
    string MenuText { get; set; }
    string Value { get; set; }
    string DisplayText { get; set; }
    object Item { get; set; }
    static TypeAheadItem MakeNewItem(string menuText = null, string value = null, string displayText = null, object item = default) =>
      new TypeAheadItem() {
        MenuText = menuText,
        Value = value,
        DisplayText = displayText,
        Item = item
      };
  }

  public interface ITypeAheadItem<TItem> : ITypeAheadItem {
    new TItem Item { get; set; }

    static TypeAheadItem<TItem> MakeNewItem(string menuText = null, string value = null, string displaytext = null, TItem item = default) =>
      new TypeAheadItem<TItem>() {
        MenuText = menuText,
        Value = value,
        DisplayText = displaytext,
        Item = item
      };

  }
}
