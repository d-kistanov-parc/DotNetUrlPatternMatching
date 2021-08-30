using System;

namespace UrlPatternMatching.Core
{
	internal class UrlPatternRequiredAuthorizationMatcher : IUrlPatternMatcher
	{
		public UrlPartType UrlPartType => UrlPartType.RequiredAuthorization;

		public void Init(Context context)
		{
		}

		public bool IsMatch(Uri url)
		{
			return !string.IsNullOrEmpty(url.UserInfo);
		}
	}
}