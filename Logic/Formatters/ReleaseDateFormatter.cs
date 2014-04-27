using System;

namespace Logic
{
	public static class ReleaseDateFormatter
	{
		public static string Format(string date)
		{
			DateTime dt = DateTime.Parse(date);
			return dt.ToString("MMM dd, yyyy");
		}
	}
}

