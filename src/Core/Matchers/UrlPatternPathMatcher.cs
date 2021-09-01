using System;

namespace UrlPatternMatching.Core
{
	internal class UrlPatternPathMatcher : BaseUrlPatternMatcher, IUrlPatternMatcher
	{
		public override UrlPartType UrlPartType => UrlPartType.Path;

		protected override bool IgnoreCase => !Context.Config.IsCaseSensitivePathMatch;

		public UrlPatternPathMatcher()
		{
			StopCharsForTilda = new[] { @"\/" };
		}

		internal override string GetValueForMatch(Uri url)
		{
			return url.LocalPath.TrimEnd('/');
		}

		internal override string PreparePatternPart(string value)
		{
			return value.TrimEnd('/');
		}
	}
}