using System.Collections.Generic;

namespace UrlPatternMatching.Core
{
    internal class Context
    {
        internal string Pattern { get; }

        internal Config Config { get; }

        internal Dictionary<UrlPartType, string> PatternParts { get; }

        internal Context(string pattern, PatternPartsParser parser, Config config)
        {
            Pattern = pattern;
            Config = config;
            PatternParts = parser.Parse(Pattern);
        }
    }
}
