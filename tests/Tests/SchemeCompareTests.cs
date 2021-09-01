using NUnit.Framework;

namespace Tests
{
    class SchemeCompareTests : BaseTests
	{
		private const string URL = "https://a.b";

		[TestCase("https://")]
		[TestCase("ht*ps://")]
		[TestCase("HT*PS://")]
		[TestCase("https://a.b")]
		[TestCase("https*://")]
		[TestCase("*https*://")]
		[TestCase("*https*://a.b")]
		[TestCase("*https*://A.B")]
		[TestCase("htt*://")]
		[TestCase("htt*://a.b")]
		[TestCase("*tt*://")]
		[TestCase("*://")]
		[TestCase("a.b")]
		[TestCase("://")]
		public void IsMatch_PatternIsValid_ShouldBeMatch(string pattern)
		{
			ShouldBeMatch(pattern, URL);
		}

		[Test]
		public void IsMatch_CheckCaseSensitive_ShouldBeIgnoreCase()
		{
			ShouldBeMatch("HTt*s://", "HttpS://a.b");
		}

		[TestCase("http://a.b")]
		[TestCase("ttps://a.b")]
		[TestCase("httpss://a.b")]
		[TestCase("tt*://a.b")]
		[TestCase("httpss://a.b")]
		[TestCase("ttp*://a.b")]
		public void IsMatch_PatternIsValid_ShouldBeNotMatch(string pattern)
		{
			ShouldBeNotMatch(pattern, URL);
		}

		[TestCase("ht~tp://a.b")]
		[TestCase("~tt*://a.b")]
		public void IsMatch_PatternIsNotValid_ShouldBeThrowException(string pattern)
		{
			ShouldBeThrowException(pattern, "\"Scheme pattern\" cannot contain \"~\", use \"*\"");
		}
	}
}