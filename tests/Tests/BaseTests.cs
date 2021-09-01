using System;
using NUnit.Framework;
using UrlPatternMatching;
using UrlPatternMatching.Core.Exceptions;

namespace Tests
{
	abstract class BaseTests
	{
		protected static void ShouldBeMatch(string pattern, string url)
		{
			Assert.IsTrue(url.IsMatch(pattern));
		}

		protected static void ShouldBeNotMatch(string pattern, string url)
		{
			Assert.IsFalse(url.IsMatch(pattern));
		}

		protected static void ShouldBeExpectedMatch(string pattern, string url, Config config, bool result)
		{
			Assert.AreEqual(result, url.IsMatch(pattern, config));
		}

		protected static void ShouldBeThrowException(string pattern, string error)
		{
			var exception = Assert.Throws<InvalidPatternException>(() => new UrlPatternMatcher(pattern));

			Assert.AreEqual(error, exception.Message);
		}

		protected static void ShouldBeComapreCaseSensetive(string url,
			string pattern,
			Config config, 
			Action<Config> changeFunc)
		{
			var urlLower = url.ToLower();
			var urlUpper = url.ToUpper();
			var patternLower = pattern.ToLower();
			var patternUpper = pattern.ToUpper();

			changeFunc(config);

			ShouldBeExpectedMatch(patternLower, urlLower, config, true);
			ShouldBeExpectedMatch(patternUpper, urlUpper, config, true);

			ShouldBeExpectedMatch(patternUpper, urlLower, config, false);
			ShouldBeExpectedMatch(patternLower, urlUpper, config, false);

			changeFunc(config);

			ShouldBeExpectedMatch(patternLower, urlLower, config, true);
			ShouldBeExpectedMatch(patternUpper, urlUpper, config, true);

			ShouldBeExpectedMatch(patternUpper, urlLower, config, true);
			ShouldBeExpectedMatch(patternLower, urlUpper, config, true);
		}

		protected static void ShouldBeComapreCaseSensetive(string url,
			string pattern,
			Action<Config> changeFunc)
		{
			ShouldBeComapreCaseSensetive(url, pattern, Config.Default, changeFunc);
		}
	}
}
