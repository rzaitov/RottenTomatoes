using System;

namespace Logic
{
	public class Review
	{
		public string critic { get; set; }
		public string date { get; set; }
		public string original_score { get; set; }
		public string freshness { get; set; }
		public string publication { get; set; }
		public string quote { get; set; }
		public Links links { get; set; }

		public bool IsFresh
		{
			get {
				return freshness == "fresh";
			}
		}

		public bool IsRotten
		{
			get {
				return freshness == "rotten";
			}
		}
	}
}

