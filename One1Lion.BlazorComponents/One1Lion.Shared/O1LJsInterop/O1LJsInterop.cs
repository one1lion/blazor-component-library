using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace One1Lion.Shared {
  public static class O1LJsInterop {
    public static ValueTask AddCssClass(IJSRuntime jsRuntime, string selector, string cssClass) {
      return jsRuntime.InvokeVoidAsync("o1lJsFunctions.addCssClass", selector, cssClass);
    }

    public static ValueTask RemoveCssClass(IJSRuntime jsRuntime, string selector, string cssClass) {
      return jsRuntime.InvokeVoidAsync("o1lJsFunctions.removeCssClass", selector, cssClass);
    }

    public static ValueTask DynamicallyLoadScript(IJSRuntime jsRuntime, string url) {
      return jsRuntime.InvokeVoidAsync("o1lJsFunctions.dynamicallyLoadScript", url);
    }

    public static ValueTask DynamicallyUnloadScript(IJSRuntime jsRuntime, string url) {
      return jsRuntime.InvokeVoidAsync("o1lJsFunctions.dynamicallyUnloadScript", url);
    }

    public static ValueTask<object> Focus(IJSRuntime jsRuntime, string elementId, bool autoSelectText = false) {
      return jsRuntime.InvokeAsync<object>(
        "o1lJsFunctions.setFocusById",
        elementId,
        autoSelectText);
    }

    public static ValueTask<object> Focus(IJSRuntime jsRuntime, ElementReference element, bool autoSelectText = false) {
      return jsRuntime.InvokeAsync<object>(
        "o1lJsFunctions.setFocus",
        element,
        autoSelectText);
    }

    public static ValueTask<string> GetUserAgent(IJSRuntime jsRuntime) {
      return jsRuntime.InvokeAsync<string>("o1lJsFunctions.getUserAgent");
    }

    public static ValueTask<bool> IsMobileDevice(IJSRuntime jsRuntime) {
      return jsRuntime.InvokeAsync<bool>("o1lJsFunctions.isMobileDevice");
    }

    public static ValueTask<object> PositionElement(IJSRuntime jsRuntime, string elementId,
                                                    int top, int right,
                                                    int bottom, int left) {
      return jsRuntime.InvokeAsync<object>(
        "o1lJsFunctions.positionElement",
        elementId,
        top,
        right,
        bottom,
        left);
    }

    public static ValueTask<object> OpenUrlInNewTab(IJSRuntime jsRuntime, string url) {
      return jsRuntime.InvokeAsync<object>("open", url, "_blank");
    }

    /// <summary>
    /// Gets the computed units of a html element by its Id
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="elementId"></param>
    /// <param name="prop"></param>
    /// <returns></returns>
    /// <remarks>
    /// This was adapted from: http://blog.moagrius.com/javascript/javascript-get-current-style-as-any-unit/
    /// </remarks>
    public static ValueTask<ElementUnits> GetUnits(IJSRuntime jsRuntime, string elementId, string prop) {
      return jsRuntime.InvokeAsync<ElementUnits>(
        "o1lJsFunctions.getUnitsById",
        elementId,
        prop);
    }

    /// <summary>
    /// Gets the computed units of a html element by its Id
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="elementId"></param>
    /// <param name="prop"></param>
    /// <returns></returns>
    /// <remarks>
    /// This was adapted from: http://blog.moagrius.com/javascript/javascript-get-current-style-as-any-unit/
    /// </remarks>
    public static ValueTask<ElementUnits> GetUnits(IJSRuntime jsRuntime, ElementReference element, string prop) {
      return jsRuntime.InvokeAsync<ElementUnits>(
        "o1lJsFunctions.getUnits",
        element,
        prop);
    }

    public static ValueTask<BoundingClientRect> GetBoundingClientRect(IJSRuntime jsRuntime, string elementId) {
      return jsRuntime.InvokeAsync<BoundingClientRect>(
        "o1lJsFunctions.getBoundingBoxById",
        elementId);
    }
    public static ValueTask<BoundingClientRect> GetBoundingClientRect(IJSRuntime jsRuntime, ElementReference element) {
      return jsRuntime.InvokeAsync<BoundingClientRect>(
        "o1lJsFunctions.getBoundingBox",
        element);
    }

    public static ValueTask MoveElementIntoView(IJSRuntime jsRuntime, string elementId) {
      return jsRuntime.InvokeVoidAsync("o1lJsFunctions.moveElementIntoView", elementId);
    }

    public static ValueTask MoveIntoView(IJSRuntime jsRuntime, ElementReference element) {
      return jsRuntime.InvokeVoidAsync("o1lJsFunctions.moveElementIntoView", element);
    }

    public static ValueTask<bool> SetInputValue(IJSRuntime jsRuntime, string elementId, object val) {
      return jsRuntime.InvokeAsync<bool>("o1lJsFunctions.setInputValueById", elementId, val);
    }

    public static ValueTask<bool> SetInputValue(IJSRuntime jsRuntime, ElementReference element, object val) {
      return jsRuntime.InvokeAsync<bool>("o1lJsFunctions.setInputValue", element, val);
    }

    public static ValueTask ScrollElementIntoView(IJSRuntime jsRuntime, string elementId) {
      return jsRuntime.InvokeVoidAsync("o1lJsFunctions.scrollElementIntoViewById", elementId);
    }

    public static ValueTask ScrollElementIntoView(IJSRuntime jsRuntime, ElementReference elemRef) {
      return jsRuntime.InvokeVoidAsync("o1lJsFunctions.scrollElementIntoView", elemRef);
    }

    public static ValueTask ShowPrintDialog(IJSRuntime jsRuntime) {
      return jsRuntime.InvokeVoidAsync("o1lJsFunctions.showPrintDialog");
    }

    public static ValueTask SetPreventSelectOnDoubleClick(IJSRuntime jsRuntime, string elementId, bool on = true) {
      return jsRuntime.InvokeVoidAsync("o1lJsFunctions.setPreventSelectOnDoubleClickById", elementId, on);
    }

    public static ValueTask SetPreventSelectOnDoubleClick(IJSRuntime jsRuntime, ElementReference element, bool on = true) {
      return jsRuntime.InvokeVoidAsync("o1lJsFunctions.setPreventSelectOnDoubleClick", element, on);
    }

    public static ValueTask<bool> ElementExists(this IJSRuntime jsRuntime, string elementId) {
      return jsRuntime.InvokeAsync<bool>("o1lJsFunctions.elementExists", elementId);
    }
  }
}
