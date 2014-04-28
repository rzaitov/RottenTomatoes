using System;

namespace Logic
{
	public static class MpaaRuntimeFormatter
	{
		public static string Format(string mpaa, int? duration)
		{
			if (duration.HasValue) {
				return string.Format("{0}, {1}", mpaa, RuntimeFormatter.Format(duration));
			}

			return mpaa;
		}
	}
}

