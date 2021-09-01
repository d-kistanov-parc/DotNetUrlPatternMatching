using System;

namespace UrlPatternMatching.Core
{
	internal class UrlPatternFragmentMatcher : BaseUrlPatternMatcher, IUrlPatternMatcher
	{
		public override UrlPartType UrlPartType => UrlPartType.Fragment;

		protected override bool IgnoreCase => !Context.Config.IsCaseSensitiveFragmentMatch;

		internal override string GetValueForMatch(Uri url)
		{
			return (url.Fragment ?? string.Empty)
				.TrimStart('#');
		}

		internal override void Validate()
		{
			ShouldNotContainTilde();
		}
	}
}