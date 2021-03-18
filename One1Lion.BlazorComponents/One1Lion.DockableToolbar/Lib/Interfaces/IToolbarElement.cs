namespace One1Lion.DockableToolbar.Interfaces {
  public interface IToolbarElement {
    bool Visible { get; set; }

    void NotifyStateChanged();
  }
}
