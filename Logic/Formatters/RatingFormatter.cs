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
			if (rating < 0)
				return string.Empty;

			return string.Format ("{0} %", rating);
		}
	}
}

