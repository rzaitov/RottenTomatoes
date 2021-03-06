﻿using System;

namespace Logic
{
	public static class RuntimeFormatter
	{
		public static string Format(int? minutes)
		{
			if (!minutes.HasValue)
				return string.Empty;

			int hours = minutes.Value / 60;
			int mins = minutes.Value % 60;

			string result = string.Format("{0} hr. {1:00} min.", hours, mins);
			return result;
		}
	}
}

