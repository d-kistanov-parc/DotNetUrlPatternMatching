using System;
using System.Linq;
using UrlPatternMatching.Core;

namespace UrlPatternMatching
{
    public sealed class UrlPatternMatcher
    {
        private readonly IUrlPatternMatcher[] _matchers;

        public UrlPatternMatcher(string pattern)
            : this(pattern, null)
        {
        }

        public UrlPatternMatcher(string pattern, Config config)
        {
            config ??= Config.Default;

            var parser = new PatternPartsParser();
            var replaceRegexBuilder = new ReplaceRegexBuilder();

            var context = new Context(pattern, parser, config);

            _matchers = new IUrlPatternMatcher[]
            {
                new UrlPatternSchemeMatcher(replaceRegexBuilder),
                new UrlPatternRequiredAuthorizationMatcher(),
                new UrlPatternUserMatcher(replaceRegexBuilder),
                new UrlPatternPasswordMatcher(replaceRegexBuilder),
                new UrlPatternHostMatcher(replaceRegexBuilder),
                new UrlPatternPortMatcher(replaceRegexBuilder),
                new UrlPatternPathMatcher(replaceRegexBuilder),
                new UrlPatternQueryMatcher(replaceRegexBuilder),
                new UrlPatternFragmentMatcher(replaceRegexBuilder)
            };

            foreach (var matcher in _matchers)
            {
                matcher.Init(context);
            }
        }

        public bool IsMatch(Uri uri)
        {
            return _matchers.All(m => m.IsMatch(uri));
        }
    }
}
