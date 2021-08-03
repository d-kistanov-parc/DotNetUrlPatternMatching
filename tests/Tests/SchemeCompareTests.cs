using NUnit.Framework;
using UrlPatternMatching;

namespace Tests
{
    class SchemeCompareTests
    {
        [TestCase("https://")]
        [TestCase("ht*ps://")]
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
        public void PositiveTest_ShouldBeMatch(string pattern)
        {
            var url = "https://a.b";
            Assert.IsTrue(url.IsMatch(pattern));
        }

        [TestCase("http://a.b")]
        [TestCase("ttps://a.b")]
        [TestCase("httpss://a.b")]
        [TestCase("tt*://a.b")]
        [TestCase("httpss://a.b")]
        [TestCase("ttp*://a.b")]
        public void NegativeTest_ShouldNotBeMatch(string pattern)
        {
            var url = "https://a.b";
            Assert.IsFalse(url.IsMatch(pattern));
        }
    }
}