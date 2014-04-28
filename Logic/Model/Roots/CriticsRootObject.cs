using System;
using System.Collections.Generic;

namespace Logic
{
	public class CriticsRootObject
	{
		public int total { get; set; }
		public List<Review> reviews { get; set; }
		public Links2 links { get; set; }
		public string link_template { get; set; }
	}
}