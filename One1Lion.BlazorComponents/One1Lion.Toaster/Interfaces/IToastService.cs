using System;

namespace One1Lion.Toaster.Interfaces {
  public interface IToastService {
    event Action<ToastContentItem> OnAdd;
    event Action<string> OnRemove;

    void Add(ToastContentItem toastContent);

    void Remove(string toastGUID);
  }
}
