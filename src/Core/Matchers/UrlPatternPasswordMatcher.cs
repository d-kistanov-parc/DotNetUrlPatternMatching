using System;
using System.Linq;

namespace UrlPatternMatching.Core
{
    internal class UrlPatternPasswordMatcher : BaseUrlPatternMatcher, IUrlPatternMatcher
    {
        public override UrlPartType UrlPartType => UrlPartType.Password;

        protected override bool IgnoreCase => !Context.Config.IsCaseSensitiveUserAndPassword;

        internal UrlPatternPasswordMatcher(ReplaceRegexBuilder regexBuilder)
            : base(regexBuilder)
        {
        }

        internal override string GetValueForMatch(Uri url)
        {
            if (string.IsNullOrEmpty(url.UserInfo))
            {
                return string.Empty;
            }

            var parts = url.Authority.Split(':');

            return parts.Length > 1
                ? parts.Last()
                : string.Empty;
        }

        internal override void Validate()
        {
            ShouldNotContainTilde();
        }
    }
}