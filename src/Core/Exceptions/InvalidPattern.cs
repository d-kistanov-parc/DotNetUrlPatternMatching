using System;

namespace UrlPatternMatching.Core.Exceptions
{
    public class InvalidPattern : Exception
    {
        public InvalidPattern(string name) : base(name)
        {
        }
    }
}