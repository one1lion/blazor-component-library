using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace One1Lion.Shared.Extensions {
  public static class StringExtensions {
    static char Char29 => Convert.ToChar(29);
    static char Char30 => Convert.ToChar(30);

    public static string Boldify(this string inputString, string findString, bool caseSensitive = false, bool matchIndividualWords = false) {
      if (!string.IsNullOrWhiteSpace(findString)) {
        if (matchIndividualWords) {
          foreach (var curString in findString.Split(" ").Distinct()) {
            inputString = Regex.Replace(inputString, @$"(?<myGroup>{curString.EscapeForRegEx()})", $"{Char29}${{myGroup}}{Char30}", !caseSensitive ? RegexOptions.IgnoreCase : RegexOptions.None);
          }
          return inputString.Replace(Char29.ToString(), "<strong>").Replace(Char30.ToString(), "</strong>");
        }
        return Regex.Replace(inputString, @$"(?<myGroup>{findString.EscapeForRegEx()})", "<strong>${myGroup}</strong>", !caseSensitive ? RegexOptions.IgnoreCase : RegexOptions.None);
      }
      return inputString;
    }

    public static string EscapeForRegEx(this string inputString) {
      return inputString.Replace(@"\", @"\\").Replace(@".", @"\.").Replace(@"$", @"\$").Replace(@"^", @"\^").Replace(@"{", @"\{").Replace(@"[", @"\[").Replace(@"(", @"\(").Replace(@"|", @"\|").Replace(@")", @"\)").Replace(@"*", @"\*").Replace(@"+", @"\+").Replace(@"?", @"\?");
    }
  }
}
