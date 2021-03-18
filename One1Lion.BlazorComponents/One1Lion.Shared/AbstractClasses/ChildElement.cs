using Microsoft.AspNetCore.Components;

namespace One1Lion.Shared {
  public abstract class ChildElement<TParent> : ComponentBase {
    [CascadingParameter] public TParent Parent { get; set; }
    bool _Visible = true;
    [Parameter]
    public bool Visible {
      get => _Visible;
      set {
        if (_Visible != value) {
          _Visible = value;
          if (VisibleChanged.HasDelegate) { VisibleChanged.InvokeAsync(_Visible); }
          StateHasChanged();
        }
      }
    }
    [Parameter] public EventCallback<bool> VisibleChanged { get; set; }

    protected override void OnInitialized() {
      base.OnInitialized();
      if (Parent is { }) {
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
