using System.Collections.Generic;

namespace UrlPatternMatching.Core
{
	internal class Context
	{
		internal string Pattern { get; }

		internal Config Config { get; }

		internal Dictionary<UrlPartType, string> PatternParts { get; }

		internal IReplaceRegexFactory ReplaceRegexFactory { get; }

		internal Context(string pattern,
			IPatternPartsParser parser,
			IReplaceRegexFactory factory,
			Config config)
		{
			Pattern = pattern;
			Config = config;
			ReplaceRegexFactory = factory;
			PatternParts = parser.Parse(Pattern);
		}
	}
}
