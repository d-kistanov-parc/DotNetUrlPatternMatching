using NUnit.Framework;

namespace Tests
{
	class FragmentCompareTests : BaseTests
	{
		[TestCase("https://a.b/p/a/t/h/#")]
		[TestCase("https://a.b/p/a/t/h/")]
		[TestCase("https://a.b/p/a/t*")]
		[TestCase("#someValue")]
		[TestCase("#some*")]
		[TestCase("#*Value")]
		public void IsMatch_PatternIsValid_ShouldBeMatch(string pattern)
		{
			var url = "https://a.b/p/a/t/h/#someValue";
			ShouldBeMatch(pattern, url);
		}

		[TestCase("#*someValue*")]
		[TestCase("#oth")]
		public void IsMatch_PatternIsValid_ShouldBeNotMatch(string pattern)
		{
			var url = "https://someValue.b/p/a/t/h/#other";
			ShouldBeNotMatch(pattern, url);
		}

		[TestCase("#молоко", "https://someValue.bu#молоко")]
		[TestCase("#молоко", "https://someValue.bu#%D0%BC%D0%BE%D0%BB%D0%BE%D0%BA%D0%BE")]
		[TestCase("#%D0%BC%D0%BE%D0%BB%D0%BE%D0%BA%D0%BE", "https://someValue.bu#молоко")]
		[TestCase("#%D0*", "https://someValue.bu#%D0%BC%D0%BE%D0%BB%D0%BE%D0%BA%D0%BE")]
		public void IsMatch_CheckAnyChars_ShouldBeMatch(string pattern, string url)
		{
			ShouldBeMatch(pattern, url);
		}

		[Test]
		public void IsMatch_CheckCaseSensitive_ShouldBeExpectedByConfig()
		{
			string pattern = "#other";
			string url = "https://someValue.b#other";

			ShouldBeComapreCaseSensetive(url, pattern, c => c.IsCaseSensitiveFragmentMatch = !c.IsCaseSensitiveFragmentMatch);
		}

		[TestCase("#~someValue")]
		[TestCase("#someValue~")]
		public void IsMatch_PatternIsNotValid_ShouldBeThrowException(string pattern)
		{
			ShouldBeThrowException(pattern, "\"Fragment pattern\" cannot contain \"~\", use \"*\"");
		}
	}
}