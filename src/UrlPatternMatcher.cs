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

			IPatternPartsParser parser = new PatternPartsParser();
			IReplaceRegexBuilder replaceRegexBuilder = new ReplaceRegexBuilder();
			IUrlPatternMatcherFactory urlPatternMatcherFactory = new UrlPatternMatcherFactory();

			var context = new Context(pattern, parser, replaceRegexBuilder, config);

			_matchers = urlPatternMatcherFactory.Create(context.PatternParts.Keys.ToArray());

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
