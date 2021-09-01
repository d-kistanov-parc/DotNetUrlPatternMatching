using System;

namespace UrlPatternMatching
{
	public static class UrlExtensions
	{
		public static bool IsMatch(this Uri url, string pattern)
		{
			return IsMatch(url, pattern, config: null);
		}

		public static bool IsMatch(this Uri url,
			string pattern,
			Config config)
		{
			var matcher = new UrlPatternMatcher(pattern, config);
			return matcher.IsMatch(url);
		}

		public static bool IsMatch(this string url, string pattern)
		{
			return IsMatch(url, pattern, config: null);
		}

		public static bool IsMatch(this string url,
			string pattern,
			Config config)
		{
			var uri = new Uri(url);
			return IsMatch(uri, pattern, config);
		}
	}
}
