using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One1Lion.Toaster {
  /// <summary>
  /// The type of Toast that determines the styling of the toast
  /// </summary>
  public enum ToastType {
    /// <summary>
    /// (Default) Applies the neutral-toast css class.  This is used for toasts that do not require attention.
    /// </summary>
    Neutral,
    /// <summary>
    /// Applies the info-toast css class.  toast is generally used for displaying informative messages 
    /// that assist a user with a task, but does not require action.
    /// </summary>
    Information,
    /// <summary>
    /// Applies the highlight-toast css class.  This toast is generally used for displaying informative 
    /// messages that draw a user's attention, but do not require action.
    /// </summary>
    Highlight,
    /// <summary>
    /// Applies the success-toast css class.  This toast is generally used for notifying the user of
    /// a successful operation.
    /// </summary>
    Success,
    /// <summary>
    /// Applies the warning-toast css class.  This toast is generally used for notifying the user of
    /// an action that may have been successful or that could result in undesired effects.
    /// </summary>
    Warning,
    /// <summary>
    /// Applies the error-toast css class.  This toast is generally used for notifying the user of
    /// an action that failed or that does have valid input.
    /// </summary>
    Error
  }

  public static class ToastTypeExtensions {
    public static string CssClass(this ToastType toastType) => toastType switch {
      ToastType.Information => "info-toast",
      ToastType.Highlight => "highlight-toast",
      ToastType.Success => "success-toast",
      ToastType.Warning => "warning-toast",
      ToastType.Error => "error-toast",
      _ => ""
    };
  }
}
