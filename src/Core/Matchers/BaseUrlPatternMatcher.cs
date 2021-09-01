using System;
using System.Linq;
using System.Text.RegularExpressions;
using UrlPatternMatching.Core.Exceptions;

namespace UrlPatternMatching.Core
{
	internal abstract class BaseUrlPatternMatcher
	{
		private Regex _compareRegex;

		protected Context Context { get; private set; }

		protected string PatternPart { get; private set; }

		public abstract UrlPartType UrlPartType { get; }

		protected abstract bool IgnoreCase { get; }

		protected virtual string[] StopCharsForTilda { get; set; } = new string[0];

		public void Init(Context context)
		{
			Context = context;

			if (context.PatternParts.TryGetValue(UrlPartType, out string patternPart))
			{				
				PatternPart = PreparePatternPart(patternPart);

				if (string.IsNullOrEmpty(PatternPart))
				{
					return;
				}

				_compareRegex = context.ReplaceRegexBuilder.ConvertPatternToRegex(PatternPart,
					IgnoreCase,
					stopCharsForTilde: StopCharsForTilda);

				Validate();
			}
		}

		public virtual bool IsMatch(Uri url)
		{
			if (string.IsNullOrEmpty(PatternPart))
			{
				return true;
			}

			var value = GetValueForMatch(url);

			var values = EncodeHelper.GetSearchValues(value);

			return values.Any(val => _compareRegex.IsMatch(val));
		}

		internal abstract string GetValueForMatch(Uri url);

		internal virtual string PreparePatternPart(string value)
		{
			return value;
		}

		internal virtual void Validate()
		{
		}

		internal void ShouldNotContainTilde()
		{
			if (PatternPart.Contains(Consts.Tilde))
			{
				throw new InvalidPatternException($"\"{UrlPartType} pattern\" cannot contain \"{Consts.Tilde}\", use \"{Consts.Asterisk}\"");
			}
		}
	}
}