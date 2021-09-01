using System.Text.RegularExpressions;

namespace UrlPatternMatching.Core
{
	internal interface IReplaceRegexBuilder
	{
		Regex ConvertPatternToRegex(string pattern, bool ignoreCase, string[] stopCharsForTilde);
	}
}