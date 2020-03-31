namespace One1Lion.BlazorComponents.DragAndDrop {
  public static class DropStyleCss {
    public const string NotSet = "";
    public const string NoDrop = "no-drop";
    public const string CanDrop = "can-drop";
    public const string GroupDrop = "group-drop";
  }

  public enum SeparatorDisplay {
    None,
    BeforeOnly,
    AfterOnly,
    Between,
    BeforeAndAfter
  }

  public enum SeparatorPosition {
    Before,
    After,
  }
}
