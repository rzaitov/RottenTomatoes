using System;

namespace Logic
{
	public static class RatingFormatter
	{
		public static string Format(int rating)
		{
			if (rating < 0)
				return string.Empty;

			return string.Format ("{0} %", rating);
		}
	}
}

