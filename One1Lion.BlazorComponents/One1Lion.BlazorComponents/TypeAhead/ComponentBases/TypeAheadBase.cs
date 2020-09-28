using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using One1Lion.BlazorComponents.SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace One1Lion.BlazorComponents.TypeAhead {
  public class TypeAheadBase : ComponentBase, IDisposable {
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; }
    [Parameter] public int Debounce { get; set; } = 300;
    [Parameter] public string SelectedText { get; set; }
    [Parameter] public string SelectedValue { get; set; }
    [Parameter] public bool SelectionOnly { get; set; }
    [Parameter] public bool LimitToList { get; set; }
    [Parameter] public EventCallback<string> SelectedTextChanged { get; set; }
    [Parameter] public EventCallback<string> SelectedValueChanged { get; set; }
    [Parameter] public EventCallback<string> OnInputChanged { get; set; }
    [Parameter] public EventCallback<string> OnTryAutoComplete { get; set; }
    [Parameter] public EventCallback<ITypeAheadItem> OnSelectionMade { get; set; }
    [Parameter] public EventCallback OnNullSelection { get; set; }
    [Parameter] public EventCallback<bool> OnDropDownVisibleChanged { get; set; }
    [Parameter] public bool LoadingList { get; set; }
    /// <summary>
    /// Invoked when the enter key is pressed.  The parameter indicates whether the
    /// suggestion list was visible when the enter key was pressed
    /// </summary>
    [Parameter] public EventCallback<bool> OnEnterKeyPressed { get; set; }
    [Parameter] public List<ITypeAheadItem> Items { get; set; }
    [Parameter] public bool CaseSensitive { get; set; }
    protected bool _IsExpanded;
    [Parameter]
    public bool IsExpanded {
      get {
        return _IsExpanded;
      }
      set {
        _IsExpanded = value;
        Task.FromResult(ReportDropDownVisibleChanged());
      }
    }

    protected ElementReference TypeAheadInputBox;

    protected string id = Guid.NewGuid().ToString();
    protected List<ITypeAheadItem> startsWithItems { get; set; } = new List<ITypeAheadItem>();
    protected List<ITypeAheadItem> containsItems { get; set; } = new List<ITypeAheadItem>();
    protected List<ITypeAheadItem> allOtherItems { get; set; }
    protected StringComparison strComp;

    protected int curHoverIndex;
    protected int totalVisSelectItems;

    protected Timer debounceTimer;
    protected string _TypeAheadInput;
    protected string TypeAheadInput {
      get => _TypeAheadInput;
      set {
        _TypeAheadInput = value;
        collapseListTimer.Stop();
        debounceTimer.Stop();
        debounceTimer.Start();
      }
    }

    protected Timer collapseListTimer;

    protected override void OnInitialized() {
      debounceTimer = new Timer() {
        Interval = Debounce,
        AutoReset = false
      };
      debounceTimer.Elapsed += HandleAutoComplete;
      collapseListTimer = new Timer() {
        Interval = 150,
        AutoReset = false
      };
      collapseListTimer.Elapsed += async (source, e) => await TrySelect(null);
      LimitToList = true;
      allOtherItems = MakeNewItemList(true);
      _TypeAheadInput = "Loading...";
    }

    protected bool typeAheadInputSet;
    protected override void OnParametersSet() {
      if (Items is { } && (string.IsNullOrWhiteSpace(_TypeAheadInput) || _TypeAheadInput == "Loading...") && !typeAheadInputSet) {
        if (!string.IsNullOrWhiteSpace(SelectedValue) && string.IsNullOrWhiteSpace(SelectedText)) {
          SelectedText = Items?.FirstOrDefault(i => i.Value == SelectedValue)?.DisplayText;
        } else if (!string.IsNullOrWhiteSpace(SelectedText) && string.IsNullOrWhiteSpace(SelectedValue)) {
          SelectedValue = Items?.FirstOrDefault(i => i.DisplayText == SelectedText)?.Value;
        }
        _TypeAheadInput = SelectedText;
        typeAheadInputSet = true;
      }
      strComp = CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
    }

    protected async void HandleAutoComplete(Object source, ElapsedEventArgs e) {
      await InvokeAsync(TryAutoComplete);
    }

    protected async Task TryAutoComplete() {
      collapseListTimer.Stop();
      curHoverIndex = 0;
      if (string.IsNullOrWhiteSpace(_TypeAheadInput)) {
        startsWithItems.Clear();
        await InvokeAsync(() => StateHasChanged());
        return;
      }

      if (OnTryAutoComplete.HasDelegate) {
        await OnTryAutoComplete.InvokeAsync(_TypeAheadInput);
      }
      if (string.IsNullOrWhiteSpace(_TypeAheadInput)) {
        return;
      }
      startsWithItems = Items.Where(i => i.MenuText.StartsWith(_TypeAheadInput, strComp)).ToList();
      containsItems = Items.Where(i => ((CaseSensitive && i.MenuText.Contains(_TypeAheadInput)) || (!CaseSensitive && i.MenuText.ToLower().Contains(_TypeAheadInput.ToLower()))) && !i.MenuText.StartsWith(_TypeAheadInput, strComp)).ToList();
      allOtherItems = new List<ITypeAheadItem>();

      await InvokeAsync(() => StateHasChanged());
    }

    protected async Task HandleKeyPress(KeyboardEventArgs e) {
      // TODO: Handle keypresses when the dropdown button or list have the focus
      // Look at https://docs.microsoft.com/en-us/aspnet/core/blazor/javascript-interop?view=aspnetcore-3.1
      //  for information on how to use the ElementReference TypeAheadInputBox to set focus or update text
      switch (e.Code) {
        case "ArrowDown":
          if (!IsExpanded) { ToggleDropDown(true); }
          ChangeSelection((int)ChangeDirection.Down);
          break;
        case "ArrowUp":
          ChangeSelection((int)ChangeDirection.Up);
          break;
        case "Escape":
          IsExpanded = false;
          curHoverIndex = 0;
          break;
        case "Enter":
        case "Tab":
          if (IsExpanded && curHoverIndex >= 0 && curHoverIndex <= totalVisSelectItems) {
            // Transform the index into the specific index for the right list:
            if (curHoverIndex < startsWithItems.Count) {
              await TrySelect(startsWithItems[curHoverIndex]);
            } else if (curHoverIndex - startsWithItems.Count < containsItems.Count) {
              var ind = curHoverIndex - startsWithItems.Count;
              await TrySelect(containsItems[ind]);
            } else if (curHoverIndex - startsWithItems.Count - containsItems.Count < allOtherItems.Count) {
              var ind = curHoverIndex - startsWithItems.Count - containsItems.Count;
              await TrySelect(allOtherItems[ind]);
            }
            if (OnEnterKeyPressed.HasDelegate && e.Code != "Tab") { await OnEnterKeyPressed.InvokeAsync(true); }
          } else if (e.Code == "Enter") {
            // Enter was pressed but an item was not pre-selected in the list, or the list is not showing
            if (OnEnterKeyPressed.HasDelegate) { await OnEnterKeyPressed.InvokeAsync(IsExpanded); }
          }
          break;
      }
    }

    protected void ChangeSelection(int which) {
      switch (which) {
        case (int)ChangeDirection.Down: // Move down
          curHoverIndex = curHoverIndex + 1;
          if (curHoverIndex >= totalVisSelectItems) { curHoverIndex = -1; }
          break;
        case (int)ChangeDirection.Up: // Move Up
          curHoverIndex = curHoverIndex - 1;
          if (curHoverIndex <= -2) { curHoverIndex = totalVisSelectItems; }
          break;
        default: // Select a specific ID by number
          curHoverIndex = Math.Max(Math.Min(which, totalVisSelectItems - 1), -1);
          break;
      }
    }

    public void ToggleDropDown(bool? overrideShow = default) {
      collapseListTimer.Stop();
      curHoverIndex = 0;
      if (string.IsNullOrWhiteSpace(_TypeAheadInput)) {
        startsWithItems = new List<ITypeAheadItem>();
        containsItems = new List<ITypeAheadItem>();
        allOtherItems = Items;
      } else {
        startsWithItems = Items.Where(i => i.MenuText.StartsWith(_TypeAheadInput, strComp)).ToList();
        containsItems = Items.Where(i => ((CaseSensitive && i.MenuText.Contains(_TypeAheadInput)) || (!CaseSensitive && i.MenuText.ToLower().Contains(_TypeAheadInput.ToLower()))) && !i.MenuText.StartsWith(_TypeAheadInput, strComp)).ToList();
        allOtherItems = Items.Where(i => !((CaseSensitive && i.MenuText.Contains(_TypeAheadInput)) || (!CaseSensitive && i.MenuText.ToLower().Contains(_TypeAheadInput.ToLower()))) && !i.MenuText.StartsWith(_TypeAheadInput, strComp)).ToList();
      }
      IsExpanded = overrideShow.HasValue ? overrideShow.Value : !IsExpanded;
    }

    public void ClearInput() {
      TypeAheadInput = null;
    }

    protected async Task TrySelect(ITypeAheadItem item) {
      collapseListTimer.Stop();
      IsExpanded = false;
      if (item is null && !string.IsNullOrWhiteSpace(_TypeAheadInput)) {
        if (LimitToList) {
          if (curHoverIndex > 0) {
            item = Items[curHoverIndex];
          } else {

            item = Items.FirstOrDefault(tai => tai.MenuText.StartsWith(_TypeAheadInput));
            if (item is null) {
              Items.FirstOrDefault(tai => tai.MenuText.Contains(_TypeAheadInput));
            }
          }
        } else {
          item = Items.FirstOrDefault(tai => tai.MenuText == _TypeAheadInput);

          if (item is null) {
            item = MakeNewItem(displayText: _TypeAheadInput, value: _TypeAheadInput, menuText: _TypeAheadInput);
          }
        }
        if (item is null) {
          _TypeAheadInput = null;
          return;
        }
      }
      curHoverIndex = 0;
      _TypeAheadInput = item?.DisplayText ?? "";
      SelectedValue = item?.Value ?? "";
      if (OnNullSelection.HasDelegate && item is null) {
        await OnNullSelection.InvokeAsync(null);
      } else if (OnSelectionMade.HasDelegate && item is { }) {
        await OnSelectionMade.InvokeAsync(item);
      }
      await InvokeAsync(() => StateHasChanged());
    }

    protected void HandleInputClicked(MouseEventArgs e) {
      IsExpanded = true;
      ToggleDropDown(true);
    }

    protected void HandleBlur() {
      // Set a timer to see if the user is clicking on an item in the dropdown,
      // or moving out of the control to another
      if (_IsExpanded) {
        collapseListTimer.Stop();
        collapseListTimer.Start();
      }
    }

    protected async Task ReportDropDownVisibleChanged() {
      if (OnDropDownVisibleChanged.HasDelegate) {
        await OnDropDownVisibleChanged.InvokeAsync(IsExpanded);
      }
    }

    List<ITypeAheadItem> MakeNewItemList(bool isLoading = false) {
      var newItem = MakeNewItem();
      if (isLoading) {
        newItem.DisplayText = "Loading...";
        newItem.MenuText = "Loading...";
        newItem.Value = "Loading...";
      }
      return new List<ITypeAheadItem> { newItem };
    }

    protected ITypeAheadItem MakeNewItem(string menuText = null, string value = null, string displayText = null, object item = default) {
      return ITypeAheadItem.MakeNewItem(menuText, value, displayText, item);
    }

    bool disposedValue;
    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          // TODO: dispose managed state (managed objects)
          if (debounceTimer is { }) { debounceTimer.Dispose(); }
          if (collapseListTimer is { }) { collapseListTimer.Dispose(); }
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

  public class TypeAheadBase<TItem> : TypeAheadBase {
    new ITypeAheadItem MakeNewItem(string menuText = null, string value = null, string displayText = null, object item = default) {
      return ITypeAheadItem<TItem>.MakeNewItem(menuText, value, displayText, item);
    }
  }
}
