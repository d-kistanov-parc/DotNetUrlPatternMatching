using System;

namespace UrlPatternMatching.Core
{
    internal class UrlPatternUserMatcher : BaseUrlPatternMatcher, IUrlPatternMatcher
    {
        public override UrlPartType UrlPartType => UrlPartType.User;

        protected override bool IgnoreCase { get => !Context.Config.IsCaseSensitiveUserAndPassword; }

        internal UrlPatternUserMatcher(ReplaceRegexBuilder regexBuilder)
            : base(regexBuilder)
        {
        }

        internal override string GetValueForMatch(Uri url)
        {
            return string.IsNullOrEmpty(url.UserInfo)
                ? string.Empty
                : url.Authority.Split(':')[0];
        }

        internal override void Validate()
        {
            ShouldNotContainTilde();
        }
    }
}