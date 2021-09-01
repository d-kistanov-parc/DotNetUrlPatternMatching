using System;

namespace UrlPatternMatching.Core
{
	internal class UrlPatternUserNameMatcher : BaseUrlPatternMatcher, IUrlPatternMatcher
	{
		public override UrlPartType UrlPartType => UrlPartType.UserName;

		protected override bool IgnoreCase { get => !Context.Config.IsCaseSensitiveUserAndPassword; }

		internal override string GetValueForMatch(Uri url)
		{
			return string.IsNullOrEmpty(url.UserInfo)
				? string.Empty
				: url.UserInfo.Split(':')[0];
		}

		internal override void Validate()
		{
			ShouldNotContainTilde();
		}
	}
}