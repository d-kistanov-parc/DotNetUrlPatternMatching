using System;

namespace UrlPatternMatching
{
	public class Config
	{
		public bool IsCaseSensitivePathMatch { get; set; }
		public bool IsCaseSensitiveFragmentMatch { get; set; }
		public bool IsCaseSensitiveParamNames { get; set; }
		public bool IsCaseSensitiveParamValues { get; set; }
		public bool IsCaseSensitiveUserAndPassword { get; set; } = true;

		public static Config Default => _default.Value;

		private static readonly Lazy<Config> _default = new Lazy<Config>(() => new Config());
	}
}
