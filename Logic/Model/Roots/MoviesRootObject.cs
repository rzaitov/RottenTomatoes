using System;
using System.Collections.Generic;

namespace Logic
{
	public class MoviesRootObject
	{
		public List<Movie> movies { get; set; }
		public Links2 links { get; set; }
		public string link_template { get; set; }
	}
}