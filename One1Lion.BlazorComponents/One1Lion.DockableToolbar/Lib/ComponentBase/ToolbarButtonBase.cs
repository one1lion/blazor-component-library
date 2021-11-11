using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using One1Lion.DockableToolbar.Interfaces;
using One1Lion.Shared;

namespace One1Lion.DockableToolbar.ComponentBases {
  public class ToolbarButtonBase : ChildElement<IToolbarContainer>, IToolbarButton {
    protected virtual string _InnerText => null;
    protected virtual string _Title => null;
    protected virtual bool _Disabled { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; }
    protected virtual string _ButtonCss => "oi oi-aperture";

    protected override void Register(IToolbarContainer parent) {
      parent.AddElement(this);
    }

    public void NotifyStateChanged() {
      StateHasChanged();
    }

    public override RenderFragment RenderContent => (builder => {
      if (Visible) {
        int i = 0;
        builder.OpenElement(i++, "button");
        builder.AddAttribute(i++, "type", "button");
        if (!string.IsNullOrWhiteSpace(_ButtonCss)) {
          builder.AddAttribute(i++, "class", _ButtonCss);
        }
        if (!string.IsNullOrWhiteSpace(_Title)) {
          builder.AddAttribute(i++, "title", _Title);
        }
        if (_Disabled) {
          builder.AddAttribute(i++, "disabled", true);
        }
        if (OnClick.HasDelegate) {
          builder.AddAttribute(i++, "onclick", OnClick);
        }
        if (AdditionalAttributes is { }) {
          foreach (var curAttribute in AdditionalAttributes) {
            builder.AddAttribute(i++, curAttribute.Key, curAttribute.Value);
          }
        }
        if (!string.IsNullOrWhiteSpace(_InnerText)) {
          builder.AddContent(i++, _InnerText);
        }
        builder.CloseElement();
      }
    });
  }
}
