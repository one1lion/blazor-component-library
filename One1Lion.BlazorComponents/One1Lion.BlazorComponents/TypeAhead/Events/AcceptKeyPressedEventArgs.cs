using Microsoft.AspNetCore.Components.Web;

namespace One1Lion.BlazorComponents.TypeAhead {
  public class AcceptKeyPressedEventArgs {
    public KeyboardEventArgs KeyboardEventArgs { get; set; }
    public bool DropDownListExpanded { get; set; }
  }
}
