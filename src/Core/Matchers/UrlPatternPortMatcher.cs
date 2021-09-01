using System;
using System.Text.RegularExpressions;
using UrlPatternMatching.Core.Exceptions;

namespace UrlPatternMatching.Core
{
	internal class UrlPatternPortMatcher : BaseUrlPatternMatcher, IUrlPatternMatcher
	{
		private static readonly Regex NotValidCharsRegex = new Regex(@$"[^\d\{Consts.Asterisk}]"
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2
, RegexOptions.Compiled
#endif
			);

		public override UrlPartType UrlPartType => UrlPartType.Port;

		protected override bool IgnoreCase => true;

		internal override string GetValueForMatch(Uri url)
		{
			return url.Port.ToString();
		}

		internal override void Validate()
		{
			if (NotValidCharsRegex.IsMatch(PatternPart))
			{
				throw new InvalidPatternException($"\"Port pattern\" can only contain numbers and \"{Consts.Asterisk}\"");
			}
		}
	}
}