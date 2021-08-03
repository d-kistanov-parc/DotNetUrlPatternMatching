using NUnit.Framework;
using System;
using UrlPatternMatching;

namespace Tests
{
    class CaseSensetiveCompareTests
    {
        [Test]
        public void Positive_AnyCase_ShouldBeExpected(
            [Values] bool urlIsUpperCase,
            [Values] bool patternIsUpperCase)
        {
            var pattern = "http*://a.b";

            if (patternIsUpperCase)
            {
                pattern = pattern.ToUpper();
            }

            var url = "https://a.b/4/f/";
            if (urlIsUpperCase)
            {
                url = url.ToUpper();
            }

            Assert.IsTrue(url.IsMatch(pattern));
        }
    }
}