using System;

namespace UrlPatternMatching.Core.Exceptions
{
	/// <summary>
	/// throw if pattern is invalid.
	/// </summary>
	public class InvalidPatternException : Exception
	{
		/// <summary>
		/// Ctor.
		/// </summary>
		/// <param name="error"></param>
		public InvalidPatternException(string error) : base(error)
		{
		}
	}
}