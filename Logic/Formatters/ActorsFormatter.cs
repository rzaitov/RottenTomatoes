using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
	public class ActorsFormatter
	{
		public ActorsFormatter ()
		{
		}

		public string Format(int take, IEnumerable<AbridgedCast> actors)
		{
			IEnumerable<string> names = actors.Select (actor => actor.name).Take (take);
			string result = string.Join (", ", names);

			return result;
		}
	}
}

