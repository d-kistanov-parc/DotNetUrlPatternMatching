using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UrlPatternMatching.Core.Exceptions;

namespace UrlPatternMatching.Core
{
    internal class UrlPatternQueryMatcher : IUrlPatternMatcher
	{
		public UrlPartType UrlPartType => UrlPartType.QueryParams;

		private List<RequiredParam> _requiredParams = null;

		public void Init(Context context)
		{
			if (context.PatternParts.TryGetValue(UrlPartType, out string patternPart))
			{
				_requiredParams = GetRequiredParams(patternPart)
					.Select(p => new RequiredParam(p, context))
					.ToList();
			}
		}

		public bool IsMatch(Uri url)
		{
			if (_requiredParams == null)
			{
				return true;
			}

			var query = (url.Query ?? string.Empty).TrimStart('?');

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

			internal Parm(string val)
			{
				var itmes = val.Split('=');

				Name = itmes[0];
				Value = itmes.Length > 1 ? itmes.Last() : string.Empty;
			}
		}

		private class RequiredParam
		{
			private readonly Regex _comapreNameRegex;
			private readonly Regex _comapreValueRegex;
			private readonly static string[] StopChars = new[] { @"&", @"=" };

			internal RequiredParam(Parm param, Context context)
			{
				if (string.IsNullOrEmpty(param.Name) || string.IsNullOrEmpty(param.Value))
				{
					throw new InvalidPatternException("\"Query pattern\" must contain the name of parameter, sign \"=\" and value. " +
						$"Instead of name or values, you can use an \"{Consts.Asterisk}\" or a combination of it and text");
				}

				if (param.Name.Contains(Consts.Tilde) || param.Value.Contains(Consts.Tilde))
				{
					throw new InvalidPatternException($"\"Query pattern\" parameter name or value cannot contain \"{Consts.Tilde}\", use \"{Consts.Asterisk}\"");
				}

				_comapreNameRegex = context.ReplaceRegexFactory.Create(param.Name,
					!context.Config.IsCaseSensitiveParamNames,
					StopChars);

				_comapreValueRegex = context.ReplaceRegexFactory.Create(param.Value,
					!context.Config.IsCaseSensitiveParamValues,
					StopChars);
			}

			internal bool IsMatched(Parm param)
			{
				var names = EncodeHelper.GetSearchValues(param.Name);
				var values = EncodeHelper.GetSearchValues(param.Value);

				return names.Any(n => _comapreNameRegex.IsMatch(n)) &&
					values.Any(v => _comapreValueRegex.IsMatch(v));
			}
		}
	}
}