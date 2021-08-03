namespace UrlPatternMatching.Core
{
    internal enum UrlPartType
    {
        Scheme,
        RequiredAuthorization,
        User,
        Password,
        Host,
        Port,
        Path,
        QueryParams,
        Fragment
    }
}
