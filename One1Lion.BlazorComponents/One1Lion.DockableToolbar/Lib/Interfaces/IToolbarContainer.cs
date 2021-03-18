namespace One1Lion.DockableToolbar.Interfaces {
  public interface IToolbarContainer {
    void AddElement(IToolbarElement childElement);
    bool HasVisibleChild { get; }
    void ChildStateChanged();
    void NotifyChildrenStateChanged();
  }
}
