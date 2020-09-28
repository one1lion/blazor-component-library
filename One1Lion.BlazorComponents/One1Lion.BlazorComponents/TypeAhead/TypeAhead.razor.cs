using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using One1Lion.BlazorComponents.JsInterop;
using One1Lion.BlazorComponents.SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timr = System.Timers;

namespace One1Lion.BlazorComponents.TypeAhead {
  public partial class TypeAhead<TItem> : ComponentBase, IDisposable {
    [Inject] public IJSRuntime JsRuntime { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; }
    [Parameter] public List<TypeAheadItem<TItem>> Items { get; set; }
    [Parameter] public int Debounce { get; set; } = 500;
    [Parameter] public bool CaseSensitive { get; set; }
    [Parameter] public bool LimitToList { get; set; } = true;
    [Parameter] public bool WrapSelection { get; set; }
    [Parameter] public bool AutoSelectTextOnFocus { get; set; }
    [Parameter] public Func<string, CancellationToken, Task<List<TypeAheadItem<TItem>>>> SuggestListLoader { get; set; }
    [Parameter] public EventCallback<string> OnTryAutoComplete { get; set; }
    [Parameter] public EventCallback<bool> OnDropDownVisibleChanged { get; set; }
    [Parameter] public EventCallback<TypeAheadItem<TItem>> OnSelectionMade { get; set; }
    [Parameter] public EventCallback<string> OnNotInList { get; set; }
    /// <summary>
    /// Invoked when the enter key is pressed.  The parameter indicates whether the
    /// suggestion list was visible when the enter key was pressed
    /// </summary>
    [Parameter] public EventCallback<bool> OnEnterKeyPressed { get; set; }
    protected bool _IsExpanded;
    [Parameter]
    public bool IsExpanded {
      get => _IsExpanded;
      set {
        _IsExpanded = value;
        InvokeAsync(() => ReportDropDownVisibleChanged());
      }
    }
    Timr.Timer collapseTimer;


    private TypeAheadItem<TItem> _SelectedTypeAheadItem;
    [Parameter]
    public TypeAheadItem<TItem> SelectedTypeAheadItem {
      get => _SelectedTypeAheadItem;
      set {
        if (_SelectedTypeAheadItem != value) {
          InvokeAsync(() => TrySelect(value));
        }
      }
    }
    [Parameter] public EventCallback<TypeAheadItem<TItem>> OnSelectedTypeAheadItemChanged { get; set; }

    [Parameter]
    public TItem SelectedItem {
      get => SelectedTypeAheadItem is null ? default : SelectedTypeAheadItem.Item;
      set {
        // Do nothing if the current SelectedTypeAheadItem is null and the value this is being set to is null as well
        if (value is null && SelectedTypeAheadItem is null) { return; }
        // Do nothing if the value this is being set to is already the value in the SelectedTypeAheadItem
        if (value is { } && SelectedTypeAheadItem is { } && value.Equals(SelectedTypeAheadItem.Item)) {
          return;
        }
        try {
          if (value is null) {
            _typeAheadInput = null;
            InvokeAsync(() => TrySelect(null));
          } else {
            InvokeAsync(() => TrySelect(new TypeAheadItem<TItem>() {
              DisplayText = "",
              MenuText = "",
              Value = "",
              Item = value
            }));
          }
        } catch (Exception) {
          Console.WriteLine($"TypeAhead: An error occurred while setting the value of SelectedItem for ta-{id}");
        }
      }
    }
    [Parameter] public EventCallback<TItem> OnSelectedItemChanged { get; set; }

    [Parameter]
    public string SelectedValue {
      get => SelectedTypeAheadItem is null ? default : SelectedTypeAheadItem.Value;
      set {
        // Do nothing if the current SelectedTypeAheadItem is null and the value this is being set to is null as well
        if (string.IsNullOrWhiteSpace(value) && SelectedTypeAheadItem is null) { return; }
        // Do nothing if the value this is being set to is already the value in the SelectedTypeAheadItem
        if (!string.IsNullOrWhiteSpace(value) && SelectedTypeAheadItem is { } && value == SelectedTypeAheadItem.Value) {
          return;
        }

        try {
          if (value is null) {
            _typeAheadInput = null;
            InvokeAsync(() => TrySelect(null));
          } else {
            InvokeAsync(() => TrySelect(new TypeAheadItem<TItem>() {
              DisplayText = "",
              MenuText = "",
              Value = value,
              Item = default
            }));
          }
        } catch (Exception) {
          Console.WriteLine($"TypeAhead: An error occurred while setting the value of SelectedValue for ta-{id}");
        }
      }
    }
    [Parameter] public EventCallback<string> OnSelectedValueChanged { get; set; }

    [Parameter]
    public string SelectedDisplayText {
      get => SelectedTypeAheadItem is null ? default : SelectedTypeAheadItem.DisplayText;
      set {
        // Do nothing if the current SelectedTypeAheadItem is null and the value this is being set to is null as well
        if (string.IsNullOrWhiteSpace(value) && SelectedTypeAheadItem is null) { return; }
        // Do nothing if the value this is being set to is already the value in the SelectedTypeAheadItem
        if (!string.IsNullOrWhiteSpace(value) && SelectedTypeAheadItem is { } && value == SelectedTypeAheadItem.DisplayText) {
          return;
        }

        try {
          if (value is null) {
            _typeAheadInput = null;
            SelectedTypeAheadItem = null;
          } else {
            InvokeAsync(() => TrySelect(new TypeAheadItem<TItem>() {
              DisplayText = value,
              MenuText = "",
              Value = "",
              Item = default
            }));
          }
        } catch (Exception) {
          Console.WriteLine($"TypeAhead: An error occurred while setting the value of SelectedDisplayText for ta-{id}");
        }
      }
    }
    [Parameter] public EventCallback<string> OnSelectedDisplayTextChanged { get; set; }

    ElementReference TypeAheadInputBox;
    StringComparison strComp;

    bool hasFocus;
    bool tryingSelect;
    bool loadingList;
    bool invalidSelection;
    bool preventCollapse;
    CancellationTokenSource loadingListCts;

    CancellationTokenSource populatingListCts;
    Timr.Timer debounceTimer;
    string _typeAheadInput;
    string typeAheadInput {
      get => _typeAheadInput;
      set {
        if (_typeAheadInput != value) {
          _typeAheadInput = value;
          debounceTimer.Stop();
          if (!tryingSelect) {
            debounceTimer.Start();
            InvokeAsync(() => UpdateLists());
          }
        }
      }
    }
    string storedTypeAheadInput;
    bool storedValue;

    int curHoverIndex = -1;
    int totalVisSelectItems;

    List<TypeAheadItem<TItem>> startsWithItems = new List<TypeAheadItem<TItem>>();
    List<TypeAheadItem<TItem>> containsItems = new List<TypeAheadItem<TItem>>();
    List<TypeAheadItem<TItem>> allOtherItems = new List<TypeAheadItem<TItem>>() {
      new TypeAheadItem<TItem>() {
        DisplayText = "Loading...",
        MenuText = "Loading...",
        Value = "Loading..."
      }
    };
    private bool disposedValue;

    protected override async Task OnInitializedAsync() {
      collapseTimer = new Timr.Timer() {
        Interval = 100,
        AutoReset = false
      };
      collapseTimer.Elapsed += HandleCollapseSuggestList;
      if (SuggestListLoader is { } && Items is { }) {
        throw new InvalidOperationException("TypeAhead requires an Items parameter, or a SuggestListLoader parameter, but not both.");
      }

      if (SuggestListLoader is { }) {
        await LoadSuggestList();
      }

      debounceTimer = new Timr.Timer() {
        Interval = Debounce,
        AutoReset = false
      };
      debounceTimer.Elapsed += HandleAutoComplete;
    }

    protected async void HandleCollapseSuggestList(Object source, Timr.ElapsedEventArgs e) {
      if (!hasFocus && !preventCollapse) {
        IsExpanded = false;
        await InvokeAsync(() => StateHasChanged());
      }
    }

    protected async void HandleAutoComplete(Object source, Timr.ElapsedEventArgs e) {
      if (SuggestListLoader is { }) {
        await LoadSuggestList();
      }
    }

    async Task HandleItemClicked(TypeAheadItem<TItem> item, int itemIndex) {
      if (itemIndex == curHoverIndex) {
        await TrySelect(item);
      } else {
        await ChangeSelection(itemIndex);
      }
    }

    protected override void OnParametersSet() {
      try {
        strComp = CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
        if (Items is { } && SuggestListLoader is null) {
          allOtherItems = Items;
        }
      } catch (Exception) {
        Console.WriteLine($"TypeAhead: An error occurred during OnParametersSet for ta-{id}");
      }
    }

    async Task UpdateLists() {
      populatingListCts?.Cancel();
      populatingListCts = new CancellationTokenSource();
      if (string.IsNullOrWhiteSpace(typeAheadInput)) { curHoverIndex = -1; }
      var thisToken = populatingListCts.Token;
      if (thisToken.IsCancellationRequested) { return; }
      startsWithItems = Items is null || string.IsNullOrWhiteSpace(typeAheadInput) ? new List<TypeAheadItem<TItem>>() : Items.Where(i => i.MenuText.StartsWith(typeAheadInput, strComp)).ToList();
      await InvokeAsync(() => StateHasChanged());
      if (thisToken.IsCancellationRequested) { return; }
      containsItems = Items is null || string.IsNullOrWhiteSpace(typeAheadInput) ? new List<TypeAheadItem<TItem>>() : Items.Where(i => ((CaseSensitive && i.MenuText.Contains(typeAheadInput)) || (!CaseSensitive && i.MenuText.ToLower().Contains(typeAheadInput.ToLower()))) && !i.MenuText.StartsWith(typeAheadInput, strComp)).ToList();
      await InvokeAsync(() => StateHasChanged());
      if (thisToken.IsCancellationRequested) { return; }
      allOtherItems = Items is null ? new List<TypeAheadItem<TItem>>()
        : string.IsNullOrWhiteSpace(typeAheadInput) ? Items
        : Items.Where(i => !startsWithItems.Contains(i) && !containsItems.Contains(i)).ToList();
      await InvokeAsync(() => StateHasChanged());
    }

    async Task HandleKeyPress(KeyboardEventArgs e) {
      invalidSelection = false;
      switch (e.Code) {
        case "ArrowDown":
          if (e.Type == "keyup") { break; }
          if (!IsExpanded) {
            curHoverIndex = -1;
          } else if (e.CtrlKey && curHoverIndex >= totalVisSelectItems - 1) {
            return;
          }

          if (e.CtrlKey) { // Ctrl + Down Arrow Key should select the last item
            curHoverIndex = totalVisSelectItems - 2;
          }
          await ChangeSelection((int)ChangeDirection.Down);
          break;
        case "ArrowUp":
          if (e.CtrlKey && curHoverIndex <= 0) {
            return;
          }

          if (e.Type == "keydown") { break; }
          if (e.CtrlKey) { // Ctrl + Up Arrow Key should select the first item
            curHoverIndex = 1;
          }

          if (IsExpanded) { await ChangeSelection((int)ChangeDirection.Up); }
          break;
        case "Escape":
          if (e.Type == "keyup") { break; }
          IsExpanded = false;
          curHoverIndex = -1;
          if (storedValue) {
            _typeAheadInput = storedTypeAheadInput;
            storedTypeAheadInput = null;
            storedValue = false;
          }
          break;
        case "Enter":
        case "NumpadEnter":
        case "Tab":
          if (e.Type == "keyup") { break; }
          if (OnEnterKeyPressed.HasDelegate) { await OnEnterKeyPressed.InvokeAsync(IsExpanded); }
          if (IsExpanded && curHoverIndex >= -1 && curHoverIndex <= totalVisSelectItems) {
            // Transform the index into the specific index for the right list:
            TypeAheadItem<TItem> taItem = null;
            if (curHoverIndex > -1) {
              if (curHoverIndex < startsWithItems.Count) {
                taItem = startsWithItems[curHoverIndex];
              } else if (curHoverIndex - startsWithItems.Count < containsItems.Count) {
                var ind = curHoverIndex - startsWithItems.Count;
                taItem = containsItems[ind];
              } else if (curHoverIndex - startsWithItems.Count - containsItems.Count < allOtherItems.Count) {
                var ind = curHoverIndex - startsWithItems.Count - containsItems.Count;
                taItem = allOtherItems[ind];
              }
            }
            await TrySelect(taItem);
          }
          break;
        case "Control":
        case "ControlRight":
        case "ControlLeft":
        case "Alt":
        case "AltRight":
        case "AltLeft":
        case "Shift":
        case "ShiftRight":
        case "ShiftLeft":
        case "ArrowLeft":
        case "ArrowRight":
          // Ignore Ctrl, Alt, and shift
          break;
        default:
          if (e.Type == "keyup") { break; }
          IsExpanded = true;
          break;
      }
    }

    protected async Task ChangeSelection(int which) {
      if (IsExpanded) { await SetFocus(); }
      var scrollIntoView = IsExpanded;
      if (!storedValue) {
        storedTypeAheadInput = _typeAheadInput;
        storedValue = true;
      }
      switch (which) {
        case (int)ChangeDirection.Down: // Move down
          curHoverIndex += 1;
          if (!IsExpanded) {
            ToggleDropDown(true);
          }
          if (curHoverIndex >= totalVisSelectItems) { curHoverIndex = WrapSelection ? 0 : totalVisSelectItems - 1; }
          break;
        case (int)ChangeDirection.Up: // Move Up
          curHoverIndex -= 1;
          if (curHoverIndex < -1) { curHoverIndex = WrapSelection ? totalVisSelectItems - 1 : -1; }
          break;
        default: // Select a specific ID by number
          curHoverIndex = Math.Max(Math.Min(which, totalVisSelectItems - 1), -1);
          break;
      }

      if (IsExpanded && curHoverIndex >= -1 && curHoverIndex <= totalVisSelectItems) {
        // Transform the index into the specific index for the right list:
        if (curHoverIndex == -1) {
          _typeAheadInput = storedTypeAheadInput;
        } else if (curHoverIndex < startsWithItems.Count) {
          _typeAheadInput = startsWithItems[curHoverIndex].DisplayText;
        } else if (curHoverIndex - startsWithItems.Count < containsItems.Count) {
          var ind = curHoverIndex - startsWithItems.Count;
          _typeAheadInput = containsItems[ind].DisplayText;
        } else if (curHoverIndex - startsWithItems.Count - containsItems.Count < allOtherItems.Count) {
          var ind = curHoverIndex - startsWithItems.Count - containsItems.Count;
          _typeAheadInput = allOtherItems[ind].DisplayText;
        }
      }
      await InvokeAsync(() => StateHasChanged());

      // Scroll the selected item into view
      try {
        if (curHoverIndex > -1) {
          if (scrollIntoView) {
            await One1LionJsInterop.ScrollElementIntoView(JsRuntime, $"ta_{curHoverIndex}");
            await InvokeAsync(() => StateHasChanged());
          }
        }
      } catch (JSException ex) {
#if DEBUG
        if (ex.Message.Contains("Could not find")) {
          Console.Error.WriteLine("Unable to scroll the active item into view since the TypeAhead Javascript reference was not added.  To enable this feature, add a reference to the TypeAhead:\r\n  <script src=\"_content/RMITSBlazor.Shared/rmitsJsInterop.js\"></script>");
        } else {
          Console.Error.WriteLine(ex);
        }
#endif
      } catch (Exception ex) {
#if DEBUG
        Console.Error.WriteLine(ex);
#endif
      }
    }

    async Task LoadSuggestList() {
      loadingListCts?.Cancel();
      loadingListCts = new CancellationTokenSource();
      var thisToken = loadingListCts.Token;
      loadingList = true;
      await InvokeAsync(() => StateHasChanged());
      try {

        var curItems = await SuggestListLoader(typeAheadInput, thisToken);
        if (thisToken.IsCancellationRequested) {
          return;
        }
        Items = curItems;
        await UpdateLists();
        loadingList = false;
      } catch (TaskCanceledException) {
        // Cancelled the current list load
      } catch (Exception ex) {
        // Another error happened
        loadingList = false;
        throw ex;
      } finally {
        await InvokeAsync(() => StateHasChanged());
      }
    }

    async Task TrySelect(TypeAheadItem<TItem> taItem) {
      tryingSelect = true;
      IsExpanded = false;
      while (loadingList) {
        await Task.Delay(500);
      }

      if (taItem is { }) {
        var taItemFound = false;
        if (!string.IsNullOrWhiteSpace(taItem.Value) && Items.Any(tai => tai.Value == taItem.Value)) {
          taItem = Items.FirstOrDefault(tai => tai.Value == taItem.Value);
          taItemFound = true;
        }
        if (!taItemFound && !string.IsNullOrWhiteSpace(taItem.DisplayText) && Items.Any(tai => tai.DisplayText == taItem.DisplayText)) {
          taItem = Items.FirstOrDefault(tai => tai.DisplayText == taItem.DisplayText);
          taItemFound = true;
        }
        if (!taItemFound && taItem.Item is { } && Items.Any(tai => taItem.Item.Equals(tai.Item))) {
          taItem = Items.FirstOrDefault(tai => taItem.Item.Equals(tai.Item));
          taItemFound = true;
        }
      }

      if (taItem is null && !string.IsNullOrWhiteSpace(_typeAheadInput)) {
        if (LimitToList) {
          if (curHoverIndex > 0) {
            taItem = Items[curHoverIndex];
          } else {
            taItem = Items.FirstOrDefault(tai => tai.MenuText.StartsWith(_typeAheadInput));
            if (taItem is null) {
              Items.FirstOrDefault(tai => tai.MenuText.Contains(_typeAheadInput));
            }
          }
        } else {
          taItem = Items.FirstOrDefault(tai => tai.MenuText == _typeAheadInput);

          if (taItem is null) {
            taItem = new TypeAheadItem<TItem>() {
              DisplayText = _typeAheadInput,
              MenuText = _typeAheadInput,
              Value = _typeAheadInput
            };
            if (OnNotInList.HasDelegate) { await OnNotInList.InvokeAsync(_typeAheadInput); }
          }
        }

        if (taItem is null) {
          invalidSelection = true;
          if (OnNotInList.HasDelegate) { await OnNotInList.InvokeAsync(_typeAheadInput); }
          return;
        }
      }
      curHoverIndex = 0;
      _typeAheadInput = taItem?.DisplayText ?? "";
      storedTypeAheadInput = null;
      storedValue = false;
      _SelectedTypeAheadItem = taItem;
      if (OnSelectionMade.HasDelegate) {
        await OnSelectionMade.InvokeAsync(taItem);
      }
      if (OnSelectedTypeAheadItemChanged.HasDelegate) { await OnSelectedTypeAheadItemChanged.InvokeAsync(_SelectedTypeAheadItem); }
      if (OnSelectedItemChanged.HasDelegate) { await OnSelectedItemChanged.InvokeAsync(SelectedItem); }
      if (OnSelectedValueChanged.HasDelegate) { await OnSelectedValueChanged.InvokeAsync(SelectedValue); }
      if (OnSelectedDisplayTextChanged.HasDelegate) { await OnSelectedDisplayTextChanged.InvokeAsync(SelectedDisplayText); }
      if (IsExpanded) { await SetFocus(); }
      await InvokeAsync(() => StateHasChanged());
      tryingSelect = false;
    }

    public void ToggleDropDown(bool? overrideShow = default) {
      //collapseListTimer.Stop();
      //curHoverIndex = 0;
      //if (string.IsNullOrWhiteSpace(_typeAheadInput)) {
      //  startsWithItems = new List<TypeAheadItem<TItem>>();
      //  containsItems = new List<TypeAheadItem<TItem>>();
      //  allOtherItems = Items;
      //} else {
      //  startsWithItems = Items.Where(i => i.MenuText.StartsWith(_typeAheadInput, strComp)).ToList();
      //  containsItems = Items.Where(i => ((CaseSensitive && i.MenuText.Contains(_typeAheadInput)) || (!CaseSensitive && i.MenuText.ToLower().Contains(_typeAheadInput.ToLower()))) && !i.MenuText.StartsWith(_typeAheadInput, strComp)).ToList();
      //  allOtherItems = Items.Where(i => !((CaseSensitive && i.MenuText.Contains(_typeAheadInput)) || (!CaseSensitive && i.MenuText.ToLower().Contains(_typeAheadInput.ToLower()))) && !i.MenuText.StartsWith(_typeAheadInput, strComp)).ToList();
      //}
      IsExpanded = overrideShow.HasValue ? overrideShow.Value : !IsExpanded;
    }

    async Task SetFocus() {
      try {
        if (!hasFocus) { await One1LionJsInterop.Focus(JsRuntime, $"typeaheadinput-{id}"); }
      } catch (JSException ex) {
#if DEBUG
        if (ex.Message.Contains("Could not find")) {
          Console.Error.WriteLine("Unable to scroll the active item into view since the TypeAhead Javascript reference was not added.  To enable this feature, add a reference to the TypeAhead:\r\n  <script src=\"_content/RMITSBlazor.Shared/rmitsJsInterop.js\"></script>");
        } else {
          Console.Error.WriteLine(ex);
        }
#endif
      } catch (Exception ex) {
#if DEBUG
        Console.Error.WriteLine(ex);
#endif
      }
    }

    void HandleFocus(FocusEventArgs e) {
      hasFocus = true;
      collapseTimer.Stop();
    }

    void HandleBlur(FocusEventArgs e) {
      hasFocus = false;
      collapseTimer.Stop();
      collapseTimer.Start();
    }

    public void ClearInput() {
      typeAheadInput = null;
      SelectedTypeAheadItem = null;
      storedTypeAheadInput = null;
      storedValue = false;
    }

    protected async Task ReportDropDownVisibleChanged() {
      if (OnDropDownVisibleChanged.HasDelegate) {
        await OnDropDownVisibleChanged.InvokeAsync(IsExpanded);
      }
    }

    void HandleOverrideClose(MouseEventArgs e) {
      if (e.Type == "mousedown") {
        preventCollapse = true;
      } else {
        preventCollapse = false;
        if (e.Type == "mouseout") {
          collapseTimer.Stop();
          collapseTimer.Start();
        }
      }
    }

    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          if (collapseTimer is { }) {
            collapseTimer.Elapsed -= HandleCollapseSuggestList;
            collapseTimer.Stop();
            collapseTimer.Dispose();
          }
          if (debounceTimer is { }) {
            debounceTimer.Elapsed -= HandleAutoComplete;
            debounceTimer.Stop();
            debounceTimer.Dispose();
          }
        }

        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        // TODO: set large fields to null
        disposedValue = true;
      }
    }

    public void Dispose() {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}
