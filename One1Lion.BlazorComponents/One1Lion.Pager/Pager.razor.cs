using Microsoft.AspNetCore.Components;
using One1Lion.Shared;
using One1Lion.Shared.Constants;
using System;
using System.Threading.Tasks;

namespace One1Lion.Pager {
  public partial class Pager {
    private string baseId = Guid.NewGuid().ToString();

    #region Parameters
    private int _CurrentPage;
    [Parameter]
    public int CurrentPage {
      get => _CurrentPage;
      set {
        if (_CurrentPage == value) { return; }
        _CurrentPage = Math.Clamp(value, 1, Math.Max(TotalPages, 1));
        if (CurrentPageChanged.HasDelegate) { CurrentPageChanged.InvokeAsync(value); }
      }
    }
    private int _TotalPages;
    [Parameter]
    public int TotalPages {
      get => _TotalPages;
      set {
        _TotalPages = Math.Max(0, value);
      }
    }

    [Parameter] public EventCallback<int> CurrentPageChanged { get; set; }
    [Parameter] public EventCallback<int> OnPage { get; set; }
    #endregion

    #region Overridden events
    protected override void OnInitialized() {
      CurrentPage = 1;
    }
    #endregion

    private async Task ChangePage(int pageNum) {
      switch (pageNum) {
        case (int)PagerDirection.First:
          CurrentPage = 1;
          break;
        case (int)PagerDirection.Previous:
          CurrentPage--;
          break;
        case (int)PagerDirection.Next:
          CurrentPage++;
          break;
        case (int)PagerDirection.Last:
          CurrentPage = TotalPages;
          break;
        default:
          CurrentPage = pageNum;
          StateHasChanged();
          await Task.Delay(30);
          await O1LJsInterop.Focus(JsRuntime, $"page{CurrentPage}-{baseId}");
          break;
      }

      if (OnPage.HasDelegate) { await OnPage.InvokeAsync(CurrentPage); }
    }
  }
}
