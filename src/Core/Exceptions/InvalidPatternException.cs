using System;

namespace UrlPatternMatching.Core.Exceptions
{
	public class InvalidPatternException : Exception
	{
		public InvalidPatternException(string name) : base(name)
		{
		}
	}
}