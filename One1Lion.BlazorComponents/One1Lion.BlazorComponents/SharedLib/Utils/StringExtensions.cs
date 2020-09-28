using System.Text.RegularExpressions;

namespace One1Lion.BlazorComponents.SharedLib.Extensions {
  public static class StringExtensions {
    public static string Boldify(this string inputString, string findString, bool caseSensitive = false) {
      if (!string.IsNullOrWhiteSpace(findString)) {
        return Regex.Replace(inputString, @$"(?<myGroup>{findString.Replace(@"\", @"\\").Replace("[", @"\[").Replace("]", @"\]").Replace("(", @"\(").Replace(")", @"\)")})", "<strong style=\"color:black;\">${myGroup}</strong>", !caseSensitive ? RegexOptions.IgnoreCase : RegexOptions.None);
      }
      return inputString;
    }
  }
}
