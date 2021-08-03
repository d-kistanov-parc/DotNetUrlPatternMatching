using System;
using System.Text.RegularExpressions;
using UrlPatternMatching.Core.Exceptions;

namespace UrlPatternMatching.Core
{
    internal abstract class BaseUrlPatternMatcher
    {
        private Regex _compareRegex;
        private ReplaceRegexBuilder _regexBuilder;

        protected Context Context { get; private set; }

        protected string PatternPart { get; private set; }

        public abstract UrlPartType UrlPartType { get; }

        protected abstract bool IgnoreCase { get; }

        protected virtual string[] StopCharsForTilda { get; set; } = new string[0];

        internal BaseUrlPatternMatcher(ReplaceRegexBuilder regexBuilder)
        {
            _regexBuilder = regexBuilder;
        }

        public void Init(Context context)
        {
            Context = context;

            if (context.PatternParts.TryGetValue(UrlPartType, out string patternPart))
            {
                PatternPart = patternPart;

                _compareRegex = _regexBuilder.ConvertPatternToRegex(PatternPart,
                    IgnoreCase,
                    stopCharsForTilde: StopCharsForTilda);

                Validate();
            }
        }

        public virtual bool IsMatch(Uri url)
        {
            if (PatternPart == null)
            {
                return true;
            }

            var value = GetValueForMatch(url);

            return _compareRegex.IsMatch(value);
        }


        internal abstract string GetValueForMatch(Uri url);

        internal virtual void Validate()         
        { 
        }

        internal void ShouldNotContainTilde()
        {
            if (PatternPart.Contains(Consts.Tilde))
            {
                throw new InvalidPattern($"{UrlPartType} cannot contain \"{Consts.Tilde}\", use \"{Consts.Asterisk}\"");
            }
        }
    }
}