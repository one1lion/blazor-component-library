using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One1Lion.Shared {
  public enum ElementPosition {
    TopLeft,
    TopCenter,
    TopRight,
    MiddleLeft,
    MiddleCenter,
    MiddleRight,
    BottomLeft,
    BottomCenter,
    BottomRight
  }

  public static class ElementPositionExtensions {
    public static string CssClass(this ElementPosition toastType) => toastType switch {
      ElementPosition.TopLeft => "top-left",
      ElementPosition.TopCenter => "top-center",
      ElementPosition.MiddleLeft => "middle-left",
      ElementPosition.MiddleCenter => "middle-center",
      ElementPosition.MiddleRight => "middle-right",
      ElementPosition.BottomLeft => "bottom-left",
      ElementPosition.BottomCenter => "bottom-center",
      ElementPosition.BottomRight => "bottom-right",
      _ => "top-right"
    };
  }
}
