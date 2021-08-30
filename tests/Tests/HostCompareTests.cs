using NUnit.Framework;

namespace Tests
{
	class HostCompareTests : BaseTests
	{
		[TestCase("://abs.biz")]
		[TestCase("abs.biz")]
		[TestCase("a~.bi~")]
		[TestCase("~s.~z")]
		[TestCase("*b*.*i*")]
		[TestCase("*bs.*i*")]
		[TestCase("abs.biz*")]
		[TestCase("abs.biz~")]
		[TestCase("*abs.biz")]
		[TestCase("~abs.biz")]
		[TestCase("*biz")]
		[TestCase("*.biz")]
		[TestCase("*iz")]
		[TestCase("ab*")]
		public void IsMatch_PatternIsValid_ShouldBeMatch(string pattern)
		{
			var url = "https://abs.biz";
			ShouldBeMatch(pattern, url);
		}

		[TestCase("://abs.bi")]
		[TestCase("abs.biza")]
		[TestCase("bs.biz")]
		[TestCase("aabs.biz")]
		[TestCase("*.abs.biz")]
		[TestCase("~iz")]
		[TestCase("~biz")]
		[TestCase("abs~")]
		[TestCase("ab~")]
		public void IsMatch_PatternIsValid_ShouldBeNotMatch(string pattern)
		{
			var url = "https://abs.biz";
			ShouldBeNotMatch(pattern, url);
		}

		[TestCase("~.168.~.2")]
		[TestCase("192.1~8.2.2")]
		[TestCase("192.1~~.2.2")]
		[TestCase("192.168.*")]
		[TestCase("192.168*")]
		[TestCase("*2.2")]
		public void IsMatch_PatternIsValidIp_ShouldBeMatch(string pattern)
		{
			var url = "https://192.168.2.2";
			ShouldBeMatch(pattern, url);
		}

		[TestCase("192.168.~")]
		[TestCase("192.168.2~")]
		[TestCase("~168.2.2")]
		[TestCase("192.*.3.2")]
		public void IsMatch_PatternIsValidIp_ShouldBeNotMatch(string pattern)
		{
			var url = "https://192.168.2.2";
			ShouldBeNotMatch(pattern, url);
		}

		[TestCase("[ffff:ffff:*]")]
		[TestCase("[ffff:ffff*]")]
		[TestCase("[ffff:ffff:ffff:~:ffff:ffff:ffff:ffff]")]
		[TestCase("[ffff:ffff:ffff:*:ffff:ffff:ffff:ffff]")]
		[TestCase("[ffff:ffff:ffff:1*4:ffff:ffff:ffff:ffff]")]
		[TestCase("[ffff:ffff:ffff:~*~:ffff:ffff:ffff:ffff]")]
		[TestCase("[~:~:~:~:~:~:~:ffff]")]
		public void IsMatch_PatternIsValidIpV6_ShouldBeMatch(string pattern)
		{
			var url = "https://[ffff:ffff:ffff:1234:ffff:ffff:ffff:ffff]";
			ShouldBeMatch(pattern, url);
		}

		[TestCase("[ffff:1fff~]")]
		[TestCase("*1*4:*")]
		public void IsMatch_PatternIsValidIpV6_ShouldBeNotMatch(string pattern)
		{
			var url = "https://[ffff:ffff:ffff:1234:ffff:ffff:ffff:ffff]";
			ShouldBeNotMatch(pattern, url);
		}

		[TestCase("ffff:ffff:ffff:*")]
		[TestCase("ffff:ffff:ffff:~")]
		[TestCase("ffff:ffff:ffff:*")]
		[TestCase("ffff:ffff:ffff:~")]
		public void IsMatch_PatternIsNotValid_ShouldBeThrowException(string pattern)
		{
			ShouldBeThrowException(pattern, "\"Host pattern\" is invalid. If you wanted to specify the ipv6 format, then use square brackets");
		}

		[Test]
		public void IsMatch_CheckCaseSensitive_ShouldBeIgnoreCase()
		{
			ShouldBeMatch("A.B.c.d", "https://a.B.c.D");
		}
	}
}