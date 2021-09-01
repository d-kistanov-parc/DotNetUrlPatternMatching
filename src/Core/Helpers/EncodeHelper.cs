using System.Collections.Generic;
using System.Net;
#if NETSTANDARD2_0 || NETSTANDARD2_0
using System.Web;
#endif

namespace UrlPatternMatching.Core
{
    internal class EncodeHelper
	{
		public static string Decode(string value)
		{
#if NETSTANDARD2_0 || NETSTANDARD2_0
				return HttpUtility.UrlDecode(value);
#else
			return WebUtility.UrlDecode(value);
#endif
		}

		internal static string Encode(string value)
		{
#if NETSTANDARD2_0 || NETSTANDARD2_0
			return HttpUtility.UrlEncode(value);
#else
			return WebUtility.UrlEncode(value);
#endif
		}

		internal static HashSet<string> GetSearchValues(string value)
		{
			return new HashSet<string>
			{
				value,
				Decode(value),
				Encode(value)
			};
		}
	}
}
