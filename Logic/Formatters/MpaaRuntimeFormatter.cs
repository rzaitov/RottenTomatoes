using System;

namespace Logic
{
	public static class MpaaRuntimeFormatter
	{
		public static string Format(string mpaa, int? duration)
		{
			if (duration.HasValue) {
				RuntimeFormatter rf = new RuntimeFormatter();
				return string.Format("{0}, {1}", mpaa, rf.Format(duration));
			}

			return mpaa;
		}
	}
}

