using One1Lion.Toaster.Interfaces;
using System;

namespace One1Lion.Toaster {
  public class ToastService : IToastService {
    public event Action<ToastContentItem> OnAdd;
    public event Action<string> OnRemove;

    public void Add(ToastContentItem toastContent) {
      OnAdd?.Invoke(toastContent);
    }

    public void Remove(string toastGUID) {
      OnRemove?.Invoke(toastGUID);
    }
  }
}
