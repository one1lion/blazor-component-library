using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Reflection;

namespace One1Lion.DockableToolbar.ComponentBase {
  public class ToolbarPopoverButtonBase<TItem> : ToolbarButtonBase {
    [Parameter] public EventCallback<TItem> OnMenuItemClicked { get; set; }

    protected virtual List<TItem> _MenuItems { get; set; }
    protected virtual PropertyInfo _MenuItemDisplayProperty => null;
    protected virtual PropertyInfo _MenuItemTitleProperty => null;

    public override RenderFragment RenderContent => (builder => {
      if (Visible) {
        int i = 0;
        builder.OpenElement(i++, "div"); // <div>
        builder.AddAttribute(i++, "class", "toolbar-popover-button");

        builder.OpenElement(i++, "button"); // <button>
        builder.AddAttribute(i++, "type", "button");
        if (!string.IsNullOrWhiteSpace(_ButtonCss)) {
          builder.AddAttribute(i++, "class", _ButtonCss);
        }
        if (_Disabled) {
          builder.AddAttribute(i++, "disabled", true);
        }
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
        if (!string.IsNullOrWhiteSpace(_InnerText)) {
          builder.AddContent(i++, _InnerText);
        }
        builder.CloseElement(); // </button>

        if ((_MenuItems?.Count ?? 0) > 0 && !_Disabled) {
          builder.OpenElement(i++, "div"); // <div>
          builder.AddAttribute(i++, "class", "popup-menu");
          builder.OpenElement(i++, "ul"); // <ul>
          foreach (var curItem in _MenuItems) {
            builder.OpenElement(i++, "li"); // <li>
            if (OnMenuItemClicked.HasDelegate) {
              builder.AddAttribute(i++, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, e => OnMenuItemClicked.InvokeAsync(curItem)));
            }
            if (_MenuItemTitleProperty is { } && !string.IsNullOrWhiteSpace(_MenuItemTitleProperty.GetValue(curItem)?.ToString())) {
              builder.AddAttribute(i++, "title", _MenuItemTitleProperty.GetValue(curItem).ToString());
            }
            if (_MenuItemDisplayProperty is { } && !string.IsNullOrWhiteSpace(_MenuItemDisplayProperty.GetValue(curItem)?.ToString())) {
              builder.AddContent(i++, _MenuItemDisplayProperty.GetValue(curItem).ToString());
            } else {
              builder.AddContent(i++, curItem);
            }
            builder.CloseElement(); // </li>
          }
          builder.CloseElement(); // </ul>
          builder.CloseElement(); // </div>
        }

        builder.CloseElement(); // </div>
      }
    });

  }
}
