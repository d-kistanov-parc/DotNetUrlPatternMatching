using NUnit.Framework;
using UrlPatternMatching;

namespace Tests
{
    class DebugTests
    {
        [Test]
        public void DebugRunTest()
        {
            var url = "https://translate.google.ru/?sl=ru&tl=en&text=%D0%BC%D0%BE%D0%BB%D0%BE%D0%BA%D0%BE&op=translate";
            Assert.IsTrue(url.IsMatch("?t*t=%D*"));
        }
    }
}