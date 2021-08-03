using NUnit.Framework;
using UrlPatternMatching;

namespace Tests
{
    class HostCompareTests
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
        public void PositiveTest_ShouldBeMatch(string pattern)
        {
            var url = "https://abs.biz";
            Assert.IsTrue(url.IsMatch(pattern));
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
        public void NegativeTest_ShouldNotBeMatch(string pattern)
        {
            var url = "https://abs.biz";
            Assert.IsFalse(url.IsMatch(pattern));
        }

        [TestCase("~.168.~.2")]
        [TestCase("192.1~8.2.2")]
        [TestCase("192.1~~.2.2")]
        [TestCase("192.168.*")]
        [TestCase("*2.2")]
        public void ComapreIp_ShouldBeMatch(string pattern)
        {
            var url = "https://192.168.2.2";
            Assert.IsTrue(url.IsMatch(pattern));
        }

        [TestCase("192.168.~")]
        [TestCase("192.168.2~")]
        [TestCase("~168.2.2")]
        [TestCase("192.*.3.2")]
        public void ComapreIp_ShouldNotBeMatch(string pattern)
        {
            var url = "https://192.168.2.2";
            Assert.IsFalse(url.IsMatch(pattern));
        }
    }
}