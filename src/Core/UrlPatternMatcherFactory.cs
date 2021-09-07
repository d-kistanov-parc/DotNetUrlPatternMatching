using System;
using System.Collections.Generic;
using System.Linq;

namespace UrlPatternMatching.Core
{
	internal class UrlPatternMatcherFactory : IUrlPatternMatcherFactory
	{
		// reflection - slow
		// DI - dependencies that are not needed
		private static readonly Dictionary<UrlPartType, Func<IUrlPatternMatcher>> Map =
			new Dictionary<UrlPartType, Func<IUrlPatternMatcher>>
			{
				{ UrlPartType.Scheme, () => new UrlPatternSchemeMatcher() },
				{ UrlPartType.RequiredAuthorization, () => new UrlPatternRequiredAuthorizationMatcher() },
				{ UrlPartType.UserName, () => new UrlPatternUserNameMatcher() },
				{ UrlPartType.UserPassword, () => new UrlPatternUserPasswordMatcher() },
				{ UrlPartType.Host, () => new UrlPatternHostMatcher() },
				{ UrlPartType.Port, () => new UrlPatternPortMatcher() },
				{ UrlPartType.Path, () => new UrlPatternPathMatcher() },
				{ UrlPartType.QueryParams, () => new UrlPatternQueryMatcher() },
				{ UrlPartType.Fragment, () => new UrlPatternFragmentMatcher() },
			};

		public IUrlPatternMatcher[] Create(UrlPartType[] types)
		{
			return types.Select(type => Map[type]()).ToArray();
		}
	}
}
