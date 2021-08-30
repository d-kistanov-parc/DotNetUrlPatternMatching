using System.Collections.Generic;

namespace UrlPatternMatching.Core
{
	internal class Context
	{
		internal string Pattern { get; }

		internal Config Config { get; }

		internal Dictionary<UrlPartType, string> PatternParts { get; }

		internal IReplaceRegexBuilder ReplaceRegexBuilder { get; }

		internal Context(string pattern,
			IPatternPartsParser parser,
			IReplaceRegexBuilder replaceRegexBuilder,
			Config config)
		{
			Pattern = pattern;
			Config = config;
			ReplaceRegexBuilder = replaceRegexBuilder;
			PatternParts = parser.Parse(Pattern);
		}
	}
}
