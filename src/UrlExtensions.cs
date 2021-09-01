using System;

namespace UrlPatternMatching
{
	/// <summary>
	/// Helpfull extensions for match url with pattern
	/// </summary>
	public static class UrlExtensions
	{
		/// <summary>
		/// Match url with pattern
		/// </summary>
		/// <param name="url">url</param>
		/// <param name="pattern">pattern</param>
		/// <returns>match result</returns>
		public static bool IsMatch(this Uri url, string pattern)
		{
			return IsMatch(url, pattern, config: null);
		}

		/// <summary>
		/// Match url with pattern
		/// </summary>
		/// <param name="url">url</param>
		/// <param name="pattern">pattern</param>
		/// <param name="config">config</param>
		/// <returns>match result</returns>
		public static bool IsMatch(this Uri url,
			string pattern,
			Config config)
		{
			var matcher = new UrlPatternMatcher(pattern, config);
			return matcher.IsMatch(url);
		}

		/// <summary>
		/// Match url with pattern
		/// </summary>
		/// <param name="url">url</param>
		/// <param name="pattern">pattern</param>
		/// <returns>match result</returns>
		public static bool IsMatch(this string url, string pattern)
		{
			return IsMatch(url, pattern, config: null);
		}

		/// <summary>
		/// Match url with pattern
		/// </summary>
		/// <param name="url">url</param>
		/// <param name="pattern">pattern</param>
		/// <param name="config">config</param>
		/// <returns>match result</returns>
		public static bool IsMatch(this string url,
			string pattern,
			Config config)
		{
			var uri = new Uri(url);
			return IsMatch(uri, pattern, config);
		}
	}
}
