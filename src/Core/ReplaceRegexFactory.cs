using System.Linq;
using System.Text.RegularExpressions;

namespace UrlPatternMatching.Core
{
	internal class ReplaceRegexFactory : IReplaceRegexFactory
	{
		public Regex Create(string pattern,
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

		private static string GetPattern(string[] stopCharsForTilda)
		{
			return stopCharsForTilda.Any()
				? $"[^{string.Join(string.Empty, stopCharsForTilda)}]*"
				: ".*";
		}
	}
}
