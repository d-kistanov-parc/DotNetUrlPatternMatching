using System;
using UrlPatternMatching.Core.Exceptions;

namespace UrlPatternMatching.Core
{
    internal class UrlPatternFragmentMatcher : BaseUrlPatternMatcher, IUrlPatternMatcher
    {
        public override UrlPartType UrlPartType => UrlPartType.Fragment;

        protected override bool IgnoreCase => !Context.Config.IsCaseSensitiveFragmentMatch;

        internal UrlPatternFragmentMatcher(ReplaceRegexBuilder regexBuilder)
            : base(regexBuilder)
        {
        }

        internal override string GetValueForMatch(Uri url)
        {
            return (url.Fragment ?? string.Empty)
                .TrimStart('#');
        }

        internal override void Validate()
        {
            if (PatternPart.Contains(Consts.Asterisk))
            {
                throw new InvalidPattern($"{UrlPartType} cannot contain \"{Consts.Asterisk}\", use \"{Consts.Tilde}\"");
            }
        }
    }
}