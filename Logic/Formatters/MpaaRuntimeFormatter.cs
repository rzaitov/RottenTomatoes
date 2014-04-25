using System;

namespace Logic
{
	public class MpaaRuntimeFormatter
	{
		public string Format(string mpaa, int? duration)
		{
			if (duration.HasValue) {
				RuntimeFormatter rf = new RuntimeFormatter();
				return string.Format("{0}, {1}", mpaa, rf.Format(duration));
			}

			return mpaa;
		}
	}
}

