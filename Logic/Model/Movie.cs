using System;
using System.Collections.Generic;

namespace Logic
{
	public class Movie
	{
		public string id { get; set; }
		public string title { get; set; }
		public int year { get; set; }
		public string mpaa_rating { get; set; }
		public int? runtime { get; set; }
		public string critics_consensus { get; set; }
		public ReleaseDates release_dates { get; set; }
		public Ratings ratings { get; set; }
		public string synopsis { get; set; }
		public Posters posters { get; set; }
		public List<AbridgedCast> abridged_cast { get; set; }
		public AlternateIds alternate_ids { get; set; }
		public Links links { get; set; }
	}
}

