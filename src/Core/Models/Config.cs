using System;

namespace UrlPatternMatching
{
	/// <summary>
	/// Config
	/// </summary>
	public class Config
	{
		/// <summary>
		/// CaseSensitive for path part
		/// </summary>
		public bool IsCaseSensitivePathMatch { get; set; }

		/// <summary>
		/// CaseSensitive for fragment part
		/// </summary>
		public bool IsCaseSensitiveFragmentMatch { get; set; }

		/// <summary>
		/// CaseSensitive for param names
		/// </summary>
		public bool IsCaseSensitiveParamNames { get; set; }

		/// <summary>
		/// CaseSensitive for param values
		/// </summary>
		public bool IsCaseSensitiveParamValues { get; set; }

		/// <summary>
		/// CaseSensitive for basic auth part
		/// </summary>
		public bool IsCaseSensitiveUserAndPassword { get; set; } = true;

		/// <summary>
		/// Global default config
		/// </summary>
		public static Config Default => _default.Value;

		private static readonly Lazy<Config> _default = new Lazy<Config>(() => new Config());
	}
}
