using System.Text.RegularExpressions;

namespace UrlPatternMatching.Core
{
    internal class ReplaceRegexBuilder
    {
        internal Regex ConvertPatternToRegex(string pattern,
            bool ignoreCase = true,
            string[] stopCharsForTilde = null)
        {
            var regexPattern = Regex.Escape(pattern);

            RegexOptions options = ignoreCase
                ? RegexOptions.IgnoreCase
                : RegexOptions.None;

            regexPattern = regexPattern
                .Replace($"\\{Consts.Asterisk}", ".*")
                .Replace(Consts.Tilde, GetPattern(stopCharsForTilde));

            return new Regex($"^{regexPattern}$", options);
        }

        private string GetPattern(string[] stopCharsForTilda)
        {
            return stopCharsForTilda.Length == 0
                ? ".*"
                : $"[^{string.Join(string.Empty, stopCharsForTilda)}]*";
        }
    }
}
