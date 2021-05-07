using System;
using System.Linq.Expressions;

namespace One1Lion.Shared.Utils {
  public static class DisplayNameExtensions {
    public static string GetDisplayName<T>(this Expression<Func<T>> expressionInfo) =>
      DisplayNameCache<T>.Current.GetDisplayName(expressionInfo);

    public static string GetDisplayName<T>(this T _, Expression<Func<T>> expressionInfo) =>
      DisplayNameCache<T>.Current.GetDisplayName(expressionInfo);
  }
}
