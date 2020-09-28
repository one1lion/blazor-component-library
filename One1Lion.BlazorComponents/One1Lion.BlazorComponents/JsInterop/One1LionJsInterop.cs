using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace One1Lion.BlazorComponents.JsInterop {
  public static class One1LionJsInterop {
    public static ValueTask<object> Focus(IJSRuntime jsRuntime, string elementId, bool autoSelectText = false) {
      return jsRuntime.InvokeAsync<object>(
        "one1lionJsFunctions.setFocusById",
        elementId,
        autoSelectText);
    }

    public static ValueTask<object> Focus(IJSRuntime jsRuntime, ElementReference element, bool autoSelectText = false) {
      return jsRuntime.InvokeAsync<object>(
        "one1lionJsFunctions.setFocus",
        element,
        autoSelectText);
    }

    public static ValueTask<object> PositionElement(IJSRuntime jsRuntime, string elementId,
                                                    int top, int right,
                                                    int bottom, int left) {
      return jsRuntime.InvokeAsync<object>(
        "one1lionJsFunctions.positionElement",
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
        "one1lionJsFunctions.getUnitsById",
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
        "one1lionJsFunctions.getUnits",
        element,
        prop);
    }

    public static ValueTask<BoundingClientRect> GetBoundingClientRect(IJSRuntime jsRuntime, string elementId) {
      return jsRuntime.InvokeAsync<BoundingClientRect>(
        "one1lionJsFunctions.getBoundingBoxById",
        elementId);
    }
    public static ValueTask<BoundingClientRect> GetBoundingClientRect(IJSRuntime jsRuntime, ElementReference element) {
      return jsRuntime.InvokeAsync<BoundingClientRect>(
        "one1lionJsFunctions.getBoundingBox",
        element);
    }

    public static ValueTask SetInputValue(IJSRuntime jsRuntime, string elementId, object val) {
      return jsRuntime.InvokeVoidAsync("one1lionJsFunctions.setInputValueById", elementId, val);
    }

    public static ValueTask SetInputValue(IJSRuntime jsRuntime, ElementReference element, object val) {
      return jsRuntime.InvokeVoidAsync("one1lionJsFunctions.setInputValue", element, val);
    }

    public static ValueTask ScrollElementIntoView(IJSRuntime jsRuntime, string elementId) {
      return jsRuntime.InvokeVoidAsync("one1lionJsFunctions.scrollElementIntoViewById", elementId);
    }

    public static ValueTask ScrollElementIntoView(IJSRuntime jsRuntime, ElementReference elemRef) {
      return jsRuntime.InvokeVoidAsync("one1lionJsFunctions.scrollElementIntoView", elemRef);
    }

    public static ValueTask ShowPrintDialog(IJSRuntime jsRuntime) {
      return jsRuntime.InvokeVoidAsync("one1lionJsFunctions.showPrintDialog");
    }
  }
}
