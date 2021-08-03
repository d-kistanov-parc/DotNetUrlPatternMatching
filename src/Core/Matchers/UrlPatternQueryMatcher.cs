using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using UrlPatternMatching.Core.Exceptions;

namespace UrlPatternMatching.Core
{
    internal class UrlPatternQueryMatcher : IUrlPatternMatcher
    {
        public UrlPartType UrlPartType => UrlPartType.QueryParams;

        private ReplaceRegexBuilder _regexBuilder;
        private List<RequiredParam> _requiredParams = null;

        internal UrlPatternQueryMatcher(ReplaceRegexBuilder regexBuilder)
        {
            _regexBuilder = regexBuilder;
        }

        public void Init(Context context)
        {
            if (context.PatternParts.TryGetValue(UrlPartType, out string patternPart))
            {
                _requiredParams = GetRequiredParams(patternPart)
                    .Select(p => new RequiredParam(p, context, _regexBuilder))
                    .ToList();
            }
        }

        public bool IsMatch(Uri url)
        {
            if (_requiredParams == null)
            {
                return true;
            }

            var query = (url.Query ?? string.Empty).Trim('?');

            List<Parm> urlParams = GetRequiredParams(query);

            foreach (RequiredParam requiredParam in _requiredParams)
            {
                bool isMatched = urlParams.Any(p => requiredParam.IsMatched(p));

                if (!isMatched)
                {
                    return isMatched;
                }
            }

            return true;
        }

        private List<Parm> GetRequiredParams(string query)
        {
            return query.Split('&')
                .Select(v => new Parm(v))
                .ToList();
        }

        private class Parm
        {
            internal string Name { get; set; }
            internal string Value { get; set; }

            internal string DecodeName { get; set; }
            internal string DecodeValue { get; set; }

            internal Parm(string val)
            {
                var itmes = val.Split('=');

                Name = itmes[0];
                Value = itmes.Length > 1 ? itmes.Last() : string.Empty;
                DecodeName = Decode(Name);
                DecodeValue = Decode(Value);
            }

            private string Decode(string value)
            {
                return HttpUtility.UrlDecode(value);
            }
        }

        private class RequiredParam
        {
            private readonly Regex _comapreNameRegex;
            private readonly Regex _comapreValueRegex;
            private readonly static string[] StopChars = new[] { @"&", @"=", "#" };

            internal RequiredParam(Parm param, Context context, ReplaceRegexBuilder regexBuilder)
            {
                if (string.IsNullOrEmpty(param.Name) || string.IsNullOrEmpty(param.Value))
                {
                    throw new InvalidPattern("\"Query Pattern\" must contain the name of parameter, sign \"=\" and value. " +
                        $"Instead of name or values, you can use an \"{Consts.Asterisk}\" or a combination of it and text");
                }

                if (string.IsNullOrEmpty(param.Name) || string.IsNullOrEmpty(param.Value))
                {
                    if (param.Name.Contains(Consts.Tilde) || param.Value.Contains(Consts.Tilde))
                    {
                        throw new InvalidPattern($"Query parameter name or value cannot contain \"{Consts.Tilde}\", use \"{Consts.Asterisk}\"");
                    }
                }

                _comapreNameRegex = regexBuilder.ConvertPatternToRegex(param.Name,
                        context.Config.IsCaseSensitiveParamNames,
                        StopChars);

                _comapreValueRegex = regexBuilder.ConvertPatternToRegex(param.Value,
                    context.Config.IsCaseSensitiveParamValues,
                    StopChars);
            }

            internal bool IsMatched(Parm param)
            {
                var isMatch = _comapreNameRegex.IsMatch(param.DecodeName) &&
                    _comapreValueRegex.IsMatch(param.DecodeValue);

                if (!isMatch && param.Name != param.DecodeName || param.Value != param.DecodeValue) 
                {
                    isMatch = _comapreNameRegex.IsMatch(param.Name) &&
                        _comapreValueRegex.IsMatch(param.Value);
                }

                return isMatch;
            }
        }
    }
}