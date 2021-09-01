using System.Collections.Generic;

namespace UrlPatternMatching.Core
{
	internal interface IPatternPartsParser
	{
		Dictionary<UrlPartType, string> Parse(string pattern);
	}
}