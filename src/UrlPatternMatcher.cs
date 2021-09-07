using System;
using System.Linq;
using UrlPatternMatching.Core;

namespace UrlPatternMatching
{
	/// <summary>
	/// Pattern container for prepare match
	/// </summary>
	public sealed class UrlPatternMatcher
	{
		private readonly IUrlPatternMatcher[] _matchers;

		/// <summary>
		/// Ctor.
		/// </summary>
		/// <param name="pattern"></param>
		public UrlPatternMatcher(string pattern)
			: this(pattern, null)
		{
		}

		/// <summary>
		/// Ctor.
		/// </summary>
		/// <param name="pattern"></param>
		/// <param name="config"></param>
		public UrlPatternMatcher(string pattern, Config config)
		{
			config ??= Config.Default;

			IPatternPartsParser parser = new PatternPartsParser();
			IReplaceRegexFactory replaceRegexFactory = new ReplaceRegexFactory();
			IUrlPatternMatcherFactory urlPatternMatcherFactory = new UrlPatternMatcherFactory();

			var context = new Context(pattern, parser, replaceRegexFactory, config);

			_matchers = urlPatternMatcherFactory.Create(context.PatternParts.Keys.ToArray());

			foreach (var matcher in _matchers)
			{
				matcher.Init(context);
			}
		}

		/// <summary>
		/// Match url with pattern
		/// </summary>
		/// <param name="url"></param>
		/// <returns>match result</returns>
		public bool IsMatch(Uri url)
		{
			return _matchers.All(m => m.IsMatch(url));
		}

		/// <summary>
		/// Match url with pattern
		/// </summary>
		/// <param name="url"></param>
		/// <returns>match result</returns>
		public bool IsMatch(string url)
		{
			return IsMatch(new Uri(url));
		}
	}
}
