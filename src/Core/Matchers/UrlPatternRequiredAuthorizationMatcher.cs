using System;

namespace UrlPatternMatching.Core
{
    internal class UrlPatternRequiredAuthorizationMatcher : IUrlPatternMatcher
    {
        private Context _context;

        public UrlPartType UrlPartType => UrlPartType.RequiredAuthorization;

        public void Init(Context context)
        {
            _context = context;
        }

        public bool IsMatch(Uri url)
        {
            return _context.PatternParts.TryGetValue(UrlPartType, out string _)
                ? !string.IsNullOrEmpty(url.UserInfo)
                : true;
        }
    }
}