using System;

namespace Logic
{
	public class Ratings
	{
		public bool IsFresh
		{
			get
			{
				if (string.IsNullOrWhiteSpace(critics_rating))
					return false;

				return critics_rating.ToLower().Contains("fresh");
			}
		}

		public string critics_rating { get; set; }
		public int critics_score { get; set; }
		public string audience_rating { get; set; }
		public int audience_score { get; set; }
	}
}

