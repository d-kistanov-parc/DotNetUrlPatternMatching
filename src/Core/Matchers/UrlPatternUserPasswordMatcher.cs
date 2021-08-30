using System;
using System.Linq;

namespace UrlPatternMatching.Core
{
	internal class UrlPatternUserPasswordMatcher : BaseUrlPatternMatcher, IUrlPatternMatcher
	{
		public override UrlPartType UrlPartType => UrlPartType.UserPassword;

		protected override bool IgnoreCase => !Context.Config.IsCaseSensitiveUserAndPassword;

		internal override string GetValueForMatch(Uri url)
		{
			if (string.IsNullOrEmpty(url.UserInfo))
			{
				return string.Empty;
			}

			var parts = url.UserInfo.Split(':');

			return parts.Length > 1
				? parts.Last()
				: string.Empty;
		}

		internal override void Validate()
		{
			ShouldNotContainTilde();
		}
	}
}