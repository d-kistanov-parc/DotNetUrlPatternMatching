using System;

namespace UrlPatternMatching.Core
{
    internal class UrlPatternPathMatcher : BaseUrlPatternMatcher, IUrlPatternMatcher
    {
        public override UrlPartType UrlPartType => UrlPartType.Path;

        protected override bool IgnoreCase => !Context.Config.IsCaseSensitivePathMatch;

        internal UrlPatternPathMatcher(ReplaceRegexBuilder regexBuilder)
            : base(regexBuilder)
        {
            StopCharsForTilda = new[] { @"\/" };
        }

        internal override string GetValueForMatch(Uri url)
        {
            return url.LocalPath;
        }
    }
}