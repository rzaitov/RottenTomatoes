using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
	public class ActorsFormatter
	{
		public string Format(int take, IEnumerable<Person> actors)
		{
			return JoinNames(actors, take);
		}

		public string Format(IEnumerable<Person> actors)
		{
			return JoinNames(actors, -1);
		}

		private string JoinNames(IEnumerable<Person> actors, int take)
		{
			IEnumerable<string> names = actors.Select(actor => actor.name);

			if (take >= 0)
				names = names.Take(take);

			return string.Join (", ", names);
		}
	}
}

