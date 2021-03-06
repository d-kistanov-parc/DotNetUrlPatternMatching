using System;

namespace UrlPatternMatching.Core
{
	internal class UrlPatternSchemeMatcher : BaseUrlPatternMatcher, IUrlPatternMatcher
	{
		public override UrlPartType UrlPartType => UrlPartType.Scheme;

		protected override bool IgnoreCase => true;

		internal override string GetValueForMatch(Uri url)
		{
			return url.Scheme;
		}

		internal override void Validate()
		{
			ShouldNotContainTilde();
		}
	}
}