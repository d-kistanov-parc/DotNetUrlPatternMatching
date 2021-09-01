namespace UrlPatternMatching.Core
{
	internal enum UrlPartType
	{
		Scheme,
		RequiredAuthorization,
		UserName,
		UserPassword,
		Host,
		Port,
		Path,
		QueryParams,
		Fragment
	}
}
