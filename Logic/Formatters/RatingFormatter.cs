using System;

namespace Logic
{
	public class RatingFormatter
	{
		public RatingFormatter ()
		{
		}

		public string Format(int rating)
		{
			return string.Format ("{0} %", rating);
		}
	}
}

