using System;
using System.Text.RegularExpressions;
using UrlPatternMatching.Core.Exceptions;

namespace UrlPatternMatching.Core
{
    internal class UrlPatternPortMatcher : BaseUrlPatternMatcher, IUrlPatternMatcher
    {
        private static readonly Regex NotValidCharsRegex = new Regex(@$"^[^\d\{Consts.Asterisk}]$", RegexOptions.Compiled);

        public override UrlPartType UrlPartType => UrlPartType.Port;

        protected override bool IgnoreCase => true;

        internal UrlPatternPortMatcher(ReplaceRegexBuilder regexBuilder)
            : base(regexBuilder)
        {
        }

        internal override string GetValueForMatch(Uri url)
        {
            return url.Port.ToString();
        }

        internal override void Validate()
        {
            if (NotValidCharsRegex.IsMatch(PatternPart))
            {
                throw new InvalidPattern($"The port can only contain numbers and \"{Consts.Asterisk}\"");
            }
        }
    }
}