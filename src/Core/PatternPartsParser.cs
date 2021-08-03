using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UrlPatternMatching.Core
{
    internal class PatternPartsParser
    {
        private readonly static Regex UrlPartsRegex = new Regex(
            @"^((?<Scheme>.*)\:\/\/)?" +
            @"(?<RequiredAuthorization>(?<User>[^:]*)(:(?<Password>.*))@)?" +
            @"(?<HostIpPort>[^\/?#]+)?" +
            @"(?<Path>\/[^\?#]+)?" +
            @"(\?(?<QueryParams>[^#]+))?" +
            @"(\#(?<Fragment>.+))?$", RegexOptions.Compiled);

        private readonly static Regex IpV6PortRegex = new Regex(@"^(?<Host>\[.+\])?(\:(?<Port>\d+))$", RegexOptions.Compiled);

        internal Dictionary<UrlPartType, string> Parse(string pattern)
        {
            var partsMap = new Dictionary<UrlPartType, string>();

            var urlPatternMatch = UrlPartsRegex.Match(pattern);

            if (urlPatternMatch.Success)
            {
                SetGroupIfExists(urlPatternMatch, UrlPartType.Scheme);
                SetGroupIfExists(urlPatternMatch, UrlPartType.RequiredAuthorization);
                SetGroupIfExists(urlPatternMatch, UrlPartType.User);
                SetGroupIfExists(urlPatternMatch, UrlPartType.Password);
                SetHost(urlPatternMatch);
                SetGroupIfExists(urlPatternMatch, UrlPartType.Path);
                SetGroupIfExists(urlPatternMatch, UrlPartType.QueryParams);
                SetGroupIfExists(urlPatternMatch, UrlPartType.Fragment);
            }

            return partsMap;

            void SetGroupIfExists(Match match, UrlPartType type)
            {
                var groupValue = GetGroupValue(match, type.ToString("g"));

                if (groupValue != null)
                {
                    partsMap.Add(type, groupValue);
                }
            }

            void SetHost(Match match)
            {
                var groupValue = GetGroupValue(match, "HostIpPort");

                if (groupValue == null)
                {
                    return;
                }

                var colonGroup = groupValue.Split(':');

                if (colonGroup.Length == 1)
                {
                    partsMap.Add(UrlPartType.Host, groupValue);
                }
                else if (colonGroup.Length == 2)
                {
                    partsMap.Add(UrlPartType.Host, colonGroup[0]);
                    partsMap.Add(UrlPartType.Port, colonGroup[1]);
                }
                else
                {
                    var ipV6PortMatch = IpV6PortRegex.Match(groupValue);

                    if (ipV6PortMatch.Success)
                    {
                        SetGroupIfExists(ipV6PortMatch, UrlPartType.Host);
                        SetGroupIfExists(ipV6PortMatch, UrlPartType.Port);
                    }
                    else
                    {
                        partsMap.Add(UrlPartType.Host, groupValue);
                    }
                }
            }
        }

        private string GetGroupValue(Match match, string name)
        {
            var group = match.Groups[name];
            return group != null && group.Success && !string.IsNullOrEmpty(group.Value)
                ? group.Value.Trim()
                : null;
        }
    }
}