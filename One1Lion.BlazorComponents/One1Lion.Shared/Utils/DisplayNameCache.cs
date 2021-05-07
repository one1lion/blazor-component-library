using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace One1Lion.Shared.Utils {
  public class DisplayNameCache<T> {
    // See: https://csharpindepth.com/articles/singleton for more info on why we do this like that.
    private static readonly Lazy<DisplayNameCache<T>> _current = new Lazy<DisplayNameCache<T>>(() => new DisplayNameCache<T>());

    public static DisplayNameCache<T> Current => _current.Value;

    private ConcurrentDictionary<MemberInfo, DisplayAttribute> _attributesMap;

    // This is done so noone knows how to make our singletons 
    private DisplayNameCache() {
      _attributesMap = new ConcurrentDictionary<MemberInfo, DisplayAttribute>();
    }

    public string GetDisplayName(Expression<Func<T>> field) {
      if (field.Body is MemberExpression memberInfo) {
        return GetDisplayName(memberInfo.Member)?.Name ?? memberInfo.Member.Name;
      }

      throw new ArgumentException("Needs to be a MemberExpression");
    }

    public DisplayAttribute GetDisplayName(MemberInfo info) {
      return _attributesMap.GetOrAdd(info,
        (keyInfo) => keyInfo.GetCustomAttribute<DisplayAttribute>(true));
    }

  }
}
