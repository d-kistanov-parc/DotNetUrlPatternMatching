using System;

namespace UrlPatternMatching.Core
{
    internal interface IUrlPatternMatcher
    {
        UrlPartType UrlPartType { get; }

        void Init(Context context);

        bool IsMatch(Uri url);
    }
}