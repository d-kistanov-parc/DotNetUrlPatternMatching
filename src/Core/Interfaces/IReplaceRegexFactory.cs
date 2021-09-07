using System.Text.RegularExpressions;

namespace UrlPatternMatching.Core
{
	internal interface IReplaceRegexFactory
	{
		Regex Create(string pattern, bool ignoreCase, string[] stopCharsForTilde);
	}
}