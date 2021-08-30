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

		internal static string Encode(string value, params string[] stopChars)
		{
			

			string result;
#if NETSTANDARD2_0 || NETSTANDARD2_0
			result = HttpUtility.UrlEncode(value);
#else
			result =  WebUtility.UrlEncode(value);
#endif

			if (stopChars != null)
			{
				foreach (var replace in stopChars)
				{
#if NETSTANDARD2_0 || NETSTANDARD2_0
					var repValue = HttpUtility.UrlEncode(replace);
#else
				var repValue = WebUtility.UrlEncode(replace);
#endif
					result = result.Replace(repValue, replace);
				}
			}

			return result;
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
