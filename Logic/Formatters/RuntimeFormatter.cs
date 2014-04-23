using System;

namespace Logic
{
	public class RuntimeFormatter
	{
		public RuntimeFormatter ()
		{
		}

		public string Format(int minutes)
		{
			int hours = minutes / 60;
			int mins = minutes % 60;

			string result = string.Format ("{0} hr. {1:00} min.", hours, mins);
			return result;
		}
	}
}

