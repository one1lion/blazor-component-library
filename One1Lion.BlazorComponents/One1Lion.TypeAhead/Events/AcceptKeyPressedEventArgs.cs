using Microsoft.AspNetCore.Components.Web;

namespace One1Lion.TypeAhead {
  public class AcceptKeyPressedEventArgs {
    public bool DropDownListExpanded { get; set; }

    public bool AltKey { get; set; }
    public string Code { get; set; }
    public bool CtrlKey { get; set; }
    public string Key { get; set; }
    public float Location { get; set; }
    public bool MetaKey { get; set; }
    public bool Repeat { get; set; }
    public bool ShiftKey { get; set; }
    public string Type { get; set; }

    public static implicit operator KeyboardEventArgs(AcceptKeyPressedEventArgs e)
        => new() {
          AltKey = e.AltKey,
          Code = e.Code,
          CtrlKey = e.CtrlKey,
          Key = e.Key,
          Location = e.Location,
          MetaKey = e.MetaKey,
          Repeat = e.Repeat,
          ShiftKey = e.ShiftKey,
          Type = e.Type
        };

    public static implicit operator AcceptKeyPressedEventArgs(KeyboardEventArgs e)
        => new() {
          AltKey = e.AltKey,
          Code = e.Code,
          CtrlKey = e.CtrlKey,
          Key = e.Key,
          Location = e.Location,
          MetaKey = e.MetaKey,
          Repeat = e.Repeat,
          ShiftKey = e.ShiftKey,
          Type = e.Type
        };
  }
}
