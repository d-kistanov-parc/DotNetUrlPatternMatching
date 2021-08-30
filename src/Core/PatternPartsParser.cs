using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UrlPatternMatching.Core.Exceptions;

namespace UrlPatternMatching.Core
{
	internal class PatternPartsParser : IPatternPartsParser
	{
		private readonly static Regex UrlPartsRegex = new Regex(
			@"^((?<Scheme>.*)\:\/\/)?" +
			@"(?<RequiredAuthorization>(?<UserName>[^:]*)(:(?<UserPassword>.*))@)?" +
			@"(?<HostIpPort>[^\/?#]*)?" +
			@"(?<Path>\/[^\?#]*)?" +
			@"(\?(?<QueryParams>[^#]*))?" +
			@"(\#(?<Fragment>.*))?$"
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2
, RegexOptions.Compiled
#endif
			);

		private readonly static Regex IpV6PortRegex = new Regex(@"^(?<Host>\[.+\])?(\:(?<Port>\d+)?)?$"
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2
, RegexOptions.Compiled
#endif
);

		public Dictionary<UrlPartType, string> Parse(string pattern)
		{
			var partsMap = new Dictionary<UrlPartType, string>();

			var urlPatternMatch = UrlPartsRegex.Match(pattern);

			if (urlPatternMatch.Success)
			{
				SetGroupIfExists(UrlPartType.Scheme);
				SetGroupIfExists(UrlPartType.RequiredAuthorization);
				SetGroupIfExists(UrlPartType.UserName);
				SetGroupIfExists(UrlPartType.UserPassword);
				SetHost(urlPatternMatch, partsMap);
				SetGroupIfExists(UrlPartType.Path);
				SetGroupIfExists(UrlPartType.QueryParams);
				SetGroupIfExists(UrlPartType.Fragment);
			}

			return partsMap;

			void SetGroupIfExists(UrlPartType type)
			{
				this.SetGroupIfExists(urlPatternMatch, type, partsMap);
			}
		}

		private string GetGroupValue(Match match, string name)
		{
			var group = match.Groups[name];
			return group != null && group.Success && !string.IsNullOrEmpty(group.Value)
				? group.Value.Trim()
				: null;
		}

		private void SetGroupIfExists(Match match, UrlPartType type, Dictionary<UrlPartType, string> partsMap)
		{
			var groupValue = GetGroupValue(match, type.ToString("g"));

			if (groupValue != null)
			{
				partsMap.Add(type, groupValue);
			}
		}

		private void SetHost(Match match, Dictionary<UrlPartType, string> partsMap)
		{
			var groupValue = GetGroupValue(match, "HostIpPort");

			if (groupValue == null)
			{
				return;
			}

			var ipV6PortMatch = IpV6PortRegex.Match(groupValue);

			if (ipV6PortMatch.Success)
			{
				SetGroupIfExists(ipV6PortMatch, UrlPartType.Host, partsMap);
				SetGroupIfExists(ipV6PortMatch, UrlPartType.Port, partsMap);
			}
			else
			{
				var colonGroup = groupValue.Split(':');

				if (colonGroup.Length == 1)
				{
					partsMap.Add(UrlPartType.Host, groupValue);
				}
				else if (colonGroup.Length == 2)
				{
					partsMap.Add(UrlPartType.Host, colonGroup[0]);

					var port = colonGroup[1];

					if (!string.IsNullOrEmpty(port))
					{
						partsMap.Add(UrlPartType.Port, port);
					}
				}
				else
				{
					throw new InvalidPatternException("\"Host pattern\" is invalid. If you wanted to specify the ipv6 format, then use square brackets");
				}
			}
		}
	}
}