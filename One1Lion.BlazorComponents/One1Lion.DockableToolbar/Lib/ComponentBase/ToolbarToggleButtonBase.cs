using Microsoft.AspNetCore.Components;

namespace One1Lion.DockableToolbar.ComponentBases {
  public class ToolbarToggleButtonBase : ToolbarButtonBase {
    [Parameter] public EventCallback<ChangeEventArgs> OnChange { get; set; }

    [Parameter] public EventCallback<bool> ToggledOnChanged { get; set; }
    [Parameter]
    public bool ToggledOn {
      get => _ToggledOn;
      set {
        if (_ToggledOn != value) {
          _ToggledOn = value;
          if (ToggledOnChanged.HasDelegate) { ToggledOnChanged.InvokeAsync(value); }
        }
      }
    }
    protected virtual bool _ToggledOn { get; set; }

    public override RenderFragment RenderContent => (builder => {
      if (Visible) {
        int i = 0;
        builder.OpenElement(i++, "label");

        var cssClass = $"toggle-button{(_Disabled ? " disabled" : "")}{(!string.IsNullOrWhiteSpace(_ButtonCss) ? $" {_ButtonCss}" : "")}";
        builder.AddAttribute(i++, "class", cssClass);
        if (!string.IsNullOrWhiteSpace(_Title)) {
          builder.AddAttribute(i++, "title", _Title);
        }
        if (OnClick.HasDelegate) {
          builder.AddAttribute(i++, "onclick", OnClick);
        }
        if (AdditionalAttributes is { }) {
          foreach (var curAttribute in AdditionalAttributes) {
            builder.AddAttribute(i++, curAttribute.Key, curAttribute.Value);
          }
        }

        builder.OpenElement(i++, "input");
        builder.AddAttribute(i++, "type", "checkbox");
        if (OnChange.HasDelegate) {
          builder.AddAttribute(i++, "onchange", OnChange);
        }
        if (_Disabled) {
          builder.AddAttribute(i++, "disabled", true);
        }
        builder.CloseElement();

        if (!string.IsNullOrWhiteSpace(_InnerText)) {
          builder.AddContent(i++, _InnerText);
        }
        builder.CloseElement();
      }
    });
  }
}
