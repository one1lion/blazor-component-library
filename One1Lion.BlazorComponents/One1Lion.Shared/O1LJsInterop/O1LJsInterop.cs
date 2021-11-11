using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace One1Lion.Shared {
  public static class O1LJsInterop {
    public static ValueTask AddCssClass(IJSRuntime jsRuntime, string selector, string cssClass) =>
        jsRuntime.InvokeVoidAsync("o1lJsFunctions.addCssClass", selector, cssClass);

    public static ValueTask RemoveCssClass(IJSRuntime jsRuntime, string selector, string cssClass) =>
      jsRuntime.InvokeVoidAsync("o1lJsFunctions.removeCssClass", selector, cssClass);

    public static ValueTask<bool> CopyTextToClipboard(IJSRuntime jsRuntime, string textToWrite) =>
      jsRuntime.InvokeAsync<bool>("o1lJsFunctions.clipboardCopy.writeString", textToWrite);

    public static ValueTask<bool> CopyElementContentToClipboard(IJSRuntime jsRuntime, ElementReference element) =>
      jsRuntime.InvokeAsync<bool>("o1lJsFunctions.clipboardCopy.writeElementContent", element);

    public static ValueTask<bool> CopyElementContentToClipboard(IJSRuntime jsRuntime, string selector) =>
      jsRuntime.InvokeAsync<bool>("o1lJsFunctions.clipboardCopy.writeElementContent", selector);

    public static ValueTask DynamicallyLoadScript(IJSRuntime jsRuntime, string url) =>
      jsRuntime.InvokeVoidAsync("o1lJsFunctions.dynamicallyLoadScript", url);

    public static ValueTask DynamicallyUnloadScript(IJSRuntime jsRuntime, string url) =>
      jsRuntime.InvokeVoidAsync("o1lJsFunctions.dynamicallyUnloadScript", url);

    public static ValueTask<object> Focus(IJSRuntime jsRuntime, string elementSelector, bool autoSelectText = false) =>
      jsRuntime.InvokeAsync<object>("o1lJsFunctions.setFocus", elementSelector, autoSelectText);

    public static ValueTask<object> Focus(IJSRuntime jsRuntime, ElementReference element, bool autoSelectText = false) =>
      jsRuntime.InvokeAsync<object>("o1lJsFunctions.setFocus", element, autoSelectText);

    public static ValueTask<string> GetUserAgent(IJSRuntime jsRuntime) =>
      jsRuntime.InvokeAsync<string>("o1lJsFunctions.getUserAgent");

    public static ValueTask<bool> IsMobileDevice(IJSRuntime jsRuntime) =>
      jsRuntime.InvokeAsync<bool>("o1lJsFunctions.isMobileDevice");

    public static ValueTask<BoundingClientRect> GetBoundingClientRect(IJSRuntime jsRuntime, string elementSelector) =>
      jsRuntime.InvokeAsync<BoundingClientRect>("o1lJsFunctions.getBoundingBox", elementSelector);

    public static ValueTask<BoundingClientRect> GetBoundingClientRect(IJSRuntime jsRuntime, ElementReference element) =>
      jsRuntime.InvokeAsync<BoundingClientRect>("o1lJsFunctions.getBoundingBox", element);

    /// <summary>
    /// Gets the selected values for the provided Html select element
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="elementSelector"></param>
    /// <returns></returns>
    public static ValueTask<List<string>> GetMultiSelectValues(IJSRuntime jsRuntime, string elementSelector) =>
      jsRuntime.InvokeAsync<List<string>>("o1lJsFunctions.getMultiSelectValues", elementSelector);

    /// <summary>
    /// Gets the selected values for the provided Html select element
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public static ValueTask<List<string>> GetMultiSelectValues(IJSRuntime jsRuntime, ElementReference element) =>
      jsRuntime.InvokeAsync<List<string>>("o1lJsFunctions.getMultiSelectValues", element);

    /// <summary>
    /// Gets the computed units of a html element by its Id
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="elementSelector"></param>
    /// <param name="prop"></param>
    /// <returns></returns>
    /// <remarks>
    /// This was adapted from: http://blog.moagrius.com/javascript/javascript-get-current-style-as-any-unit/
    /// </remarks>
    public static ValueTask<ElementUnits> GetUnits(IJSRuntime jsRuntime, string elementSelector, string prop) =>
      jsRuntime.InvokeAsync<ElementUnits>("o1lJsFunctions.getUnits", elementSelector, prop);

    /// <summary>
    /// Gets the computed units of a html element by its Id
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="elementSelector"></param>
    /// <param name="prop"></param>
    /// <returns></returns>
    /// <remarks>
    /// This was adapted from: http://blog.moagrius.com/javascript/javascript-get-current-style-as-any-unit/
    /// </remarks>
    public static ValueTask<ElementUnits> GetUnits(IJSRuntime jsRuntime, ElementReference element, string prop) =>
      jsRuntime.InvokeAsync<ElementUnits>("o1lJsFunctions.getUnits", element, prop);

    public static ValueTask LocalSaveAs(IJSRuntime js, string filename, byte[] data) =>
      js.InvokeVoidAsync("o1lJsFunctions.saveAsFile", filename, Convert.ToBase64String(data));

    public static ValueTask LocalSaveAs(IJSRuntime js, string filename, string data) =>
      LocalSaveAs(js, filename, System.Text.Encoding.UTF8.GetBytes(data));

    public static ValueTask MoveElementIntoView(IJSRuntime jsRuntime, string elementSelector) =>
      jsRuntime.InvokeVoidAsync("o1lJsFunctions.moveElementIntoView", elementSelector);

    public static ValueTask MoveIntoView(IJSRuntime jsRuntime, ElementReference element) =>
      jsRuntime.InvokeVoidAsync("o1lJsFunctions.moveElementIntoView", element);

    public static ValueTask<object> OpenUrl(IJSRuntime jsRuntime, string url, string target = "_blank") =>
      jsRuntime.InvokeAsync<object>("o1lJsFunctions.openUrl", url, target);

    [Obsolete("OpenUrlInNewTab is deprecated, please use OpenUrl instead.")]
    public static ValueTask<object> OpenUrlInNewTab(IJSRuntime jsRuntime, string url) =>
      jsRuntime.InvokeAsync<object>("open", url, "_blank");

    public static ValueTask<object> PositionElement(IJSRuntime jsRuntime, string elementSelector,
                                                    int top, int right,
                                                    int bottom, int left) =>
      jsRuntime.InvokeAsync<object>("o1lJsFunctions.positionElement", elementSelector, top, right, bottom, left);

    public static ValueTask<bool> SetInputValue(IJSRuntime jsRuntime, string elementSelector, object val) =>
      jsRuntime.InvokeAsync<bool>("o1lJsFunctions.setInputValue", elementSelector, val);

    public static ValueTask<bool> SetInputValue(IJSRuntime jsRuntime, ElementReference element, object val) =>
      jsRuntime.InvokeAsync<bool>("o1lJsFunctions.setInputValue", element, val);

    public static ValueTask ScrollElementIntoView(IJSRuntime jsRuntime, string elementSelector) =>
      jsRuntime.InvokeVoidAsync("o1lJsFunctions.scrollElementIntoView", elementSelector);

    public static ValueTask ScrollElementIntoView(IJSRuntime jsRuntime, ElementReference elemRef) =>
      jsRuntime.InvokeVoidAsync("o1lJsFunctions.scrollElementIntoView", elemRef);

    public static ValueTask ShowPrintDialog(IJSRuntime jsRuntime) =>
      jsRuntime.InvokeVoidAsync("o1lJsFunctions.showPrintDialog");

    public static ValueTask SetPreventSelectOnDoubleClick(IJSRuntime jsRuntime, string elementSelector, bool on = true) =>
      jsRuntime.InvokeVoidAsync("o1lJsFunctions.setPreventSelectOnDoubleClick", elementSelector, on);

    public static ValueTask SetPreventSelectOnDoubleClick(IJSRuntime jsRuntime, ElementReference element, bool on = true) =>
      jsRuntime.InvokeVoidAsync("o1lJsFunctions.setPreventSelectOnDoubleClick", element, on);

    public static ValueTask<bool> ElementExists(this IJSRuntime jsRuntime, string elementSelector) =>
      jsRuntime.InvokeAsync<bool>("o1lJsFunctions.elementExists", elementSelector);
  }
}
