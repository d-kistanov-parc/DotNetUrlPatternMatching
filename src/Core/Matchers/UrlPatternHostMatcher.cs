using System;

namespace UrlPatternMatching.Core
{
	internal class UrlPatternHostMatcher : BaseUrlPatternMatcher, IUrlPatternMatcher
	{
		public override UrlPartType UrlPartType => UrlPartType.Host;

		protected override bool IgnoreCase => true;

		public UrlPatternHostMatcher()
		{
			StopCharsForTilda = new[] { @"\." };
		}

		internal override string GetValueForMatch(Uri url)
		{
			return url.Host;
		}
	}
}