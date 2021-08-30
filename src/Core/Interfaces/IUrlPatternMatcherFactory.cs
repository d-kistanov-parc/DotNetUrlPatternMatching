namespace UrlPatternMatching.Core
{
	internal interface IUrlPatternMatcherFactory
	{
		IUrlPatternMatcher[] Create(UrlPartType[] types);
	}
}