using NUnit.Framework;

namespace Tests
{
	class PortCompareTests : BaseTests
	{
		private const string URL = "https://a.b:1234";

		[TestCase("*:1234")]
		[TestCase("*:12*")]
		[TestCase("*:*1234")]
		[TestCase("a.b:1234")]
		[TestCase("a.b:")]
		[TestCase("*:*")]
		[TestCase("*")]
		[TestCase(":")]
		public void IsMatch_PatternIsValid_ShouldBeMatch(string pattern)
		{
			ShouldBeMatch(pattern, URL);
		}

		[TestCase("*:12345")]
		[TestCase("*:123*45")]
		public void IsMatch_PatternIsValid_ShouldBeNotMatch(string pattern)
		{
			ShouldBeNotMatch(pattern, URL);
		}

		[TestCase("*:b44")]
		[TestCase("*:123~")]
		public void IsMatch_PatternIsNotValid_ShouldBeThrowException(string pattern)
		{
			ShouldBeThrowException(pattern, "\"Port pattern\" can only contain numbers and \"*\"");
		}
	}
}