using NUnit.Framework;
using UrlPatternMatching;

namespace Tests
{
	class QueryCompareTests : BaseTests
	{
		[TestCase("https://a.b/?cc=33&aa=11")]
		[TestCase("https://a.b/?c*=33&*a=11")]
		[TestCase("https://a.b/?cc=*3&aa=1*")]
		[TestCase("https://a.b/?*=33")]
		[TestCase("https://a.b/?")]
		public void IsMatch_PatternIsValid_ShouldBeMatch(string pattern)
		{
			var url = "https://a.b/?aa=11&bb=22&cc=33";
			ShouldBeMatch(pattern, url);
		}

		[TestCase("https://a.b/?text=%D*")]
		[TestCase("https://a.b/?text=мол")]
		[TestCase("https://a.b/?text=%D0%BC%D0%BE%D0%BB")]
		public void IsMatch_EscapeCharacter_ShouldBeMatch(string pattern)
		{
			var url = "https://a.b/?text=%D0%BC%D0%BE%D0%BB&op=translate";
			ShouldBeMatch(pattern, url);
		}

		[TestCase(true, "?MyParam=One&Other=Two", true, true)]
		[TestCase(true, "?MyParam=One&Other=Two", false, false)]
		[TestCase(false, "?MyParam=One&other=Two", true, false)]
		[TestCase(false, "?MyParam=One&Other=two", false, true)]
		[TestCase(true, "?MyParam=One&Other=two", true, false)]
		[TestCase(true, "?MyParam=One&other=Two", false, true)]
		public void IsMatch_CheckCaseSensitive_ShouldBeExpectedByConfig(
			bool expected,
			string pattern,
			bool isCaseSensitiveParamNames,
			bool isCaseSensitiveParamValues)
		{
			var url = "https://a.b/?MyParam=One&Other=Two";

			var config = new Config
			{
				IsCaseSensitiveParamNames = isCaseSensitiveParamNames,
				IsCaseSensitiveParamValues = isCaseSensitiveParamValues
			};

			ShouldBeExpectedMatch(pattern, url, config, expected);
		}

		[TestCase("https://a.b/?cc=333&aa=11")]
		[TestCase("https://a.b/?aa=11&bb=22&cc=33&dd=44")]
		public void IsMatch_PatternIsValid_ShouldBeNotMatch(string pattern)
		{
			var url = "https://a.b/?aa=11&bb=22&cc=33";
			ShouldBeNotMatch(pattern, url);
		}

		[TestCase("?MyParam=One~")]
		[TestCase("?MyParam~=One")]
		[TestCase("?MyPa~ram=~One")]
		[TestCase("?~MyParam=One")]
		public void IsMatch_PatternIsNotValid_ShouldBeThrowException(string pattern)
		{
			ShouldBeThrowException(pattern, "\"Query pattern\" parameter name or value cannot contain \"~\", use \"*\"");
		}

		[TestCase("?MyParam=One&n=")]
		[TestCase("?MyParam=One&=3")]
		public void IsMatch_ParamNameOrValueIsEmpty_ShouldBeThrowException(string pattern)
		{
			ShouldBeThrowException(pattern, "\"Query pattern\" must contain the name of parameter, sign \"=\" and value. " +
				$"Instead of name or values, you can use an \"*\" or a combination of it and text");
		}
	}
}