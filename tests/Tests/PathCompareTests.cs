using NUnit.Framework;

namespace Tests
{
	class PathCompareTests : BaseTests
	{
		private const string URL = "https://a.b/abc/def/";

		[TestCase("/abc/def")]
		[TestCase("/abc/~")]
		[TestCase("/abc/d~")]
		[TestCase("/abc/~ef")]
		[TestCase("/*abc/def")]
		[TestCase("/~/~ef")]
		[TestCase("/*/~ef")]
		[TestCase("/a*/~ef")]
		[TestCase("/*c/~ef")]
		[TestCase("/*")]
		[TestCase("a.b")]
		[TestCase("a.b/")]
		public void IsMatch_PatternIsValid_ShouldBeMatch(string pattern)
		{
			ShouldBeMatch(pattern, URL);
		}

		[TestCase("/abc/deff")]
		[TestCase("/def")]
		[TestCase("/def/")]
		[TestCase("/c/abc/deff")]
		[TestCase("/~/abc/def")]
		[TestCase("/*/abc/def")]
		[TestCase("/c/abc/def/any")]
		public void IsMatch_PatternIsValid_ShouldBeNotMatch(string pattern)
		{
			ShouldBeNotMatch(pattern, URL);
		}

		[Test]
		public void IsMatch_CheckCaseSensitive_ShouldBeExpectedByConfig()
		{
			string pattern = "/abc/def";
			ShouldBeComapreCaseSensetive(URL, pattern, c => c.IsCaseSensitivePathMatch = !c.IsCaseSensitivePathMatch);
		}
	}
}