using Microsoft.AspNetCore.Components;
namespace One1Lion.BlazorComponents.SharedLib {
  public abstract class ChildElement<TParent> : ComponentBase {
    [CascadingParameter] TParent Parent { get; set; }

    protected override void OnInitialized() {
      base.OnInitialized();
      if(Parent is { }) {
        Register(Parent);
      }
    }

    protected abstract void Register(TParent parent);
    public abstract RenderFragment RenderContent { get; }

    protected new void StateHasChanged() {
      base.StateHasChanged();
      var cscHandler = Parent as IHandleChildStateChanges;
      if (cscHandler is { }) {
        cscHandler.ChildStateChanged();
      }
    }
  }
}
