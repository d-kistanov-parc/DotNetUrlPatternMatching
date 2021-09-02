using NUnit.Framework;
using UrlPatternMatching;

namespace Tests
{
	class AuthCompareTests : BaseTests
	{
		private const string URL = "https://us:pas@a.b";

		[TestCase("https://us:pas@a.b")]
		[TestCase("https://us:@a.b")]
		[TestCase("https://:pas@a.b")]
		[TestCase("https://:@a.b")]
		[TestCase("https://*:*@a.b")]
		[TestCase("https://u*:pas@a.b")]
		[TestCase("https://us:pas*@a.b")]
		public void IsMatch_PatternIsValid_ShouldBeMatch(string pattern)
		{
			ShouldBeMatch(pattern, URL);
		}

		[TestCase("https://user:pas@a.b")]
		[TestCase("https://usert:@a.b")]
		[TestCase("https://pas:us@a.b")]
		[TestCase("https://*us:*pasw@a.b")]
		public void IsMatch_PatternIsValid_ShouldBeNotMatch(string pattern)
		{
			ShouldBeNotMatch(pattern, URL);
		}

		[Test]
		public void IsMatch_CheckCaseSensitive_ShouldBeExpectedByConfig()
		{
			string pattern = "https://us:pas@a.b";

			var config = new Config { IsCaseSensitiveUserAndPassword = false };
			ShouldBeComapreCaseSensetive(URL, pattern, config, c => c.IsCaseSensitiveUserAndPassword = !c.IsCaseSensitiveUserAndPassword);
		}

		[TestCase("https://@a.b")]
		[TestCase("https://a.b")]
		public void IsMatch_UrlWithoutAuthInfoAndPatternAuthIsRequired_ShouldBeNotMatch(string url)
		{
			var pattern = "https://:@a.b";
			ShouldBeNotMatch(pattern, url);
		}

		[TestCase("https://u*:p~@a.b", "\"UserPassword pattern\" cannot contain \"~\", use \"*\"")]
		[TestCase("https://u~:p*@a.b", "\"UserName pattern\" cannot contain \"~\", use \"*\"")]
		public void IsMatch_PatternIsNotValid_ShouldBeThrowException(string pattern, string error)
		{
			ShouldBeThrowException(pattern, error);
		}
	}
}