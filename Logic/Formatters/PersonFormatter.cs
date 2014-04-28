using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
	public static class PersonFormatter
	{
		public static string Format(int take, IEnumerable<Person> actors)
		{
			return JoinNames(actors, take);
		}

		public static string Format(IEnumerable<Person> actors)
		{
			return JoinNames(actors, -1);
		}

		private static string JoinNames(IEnumerable<Person> actors, int take)
		{
			IEnumerable<string> names = actors.Select(actor => actor.name);

			if (take >= 0)
				names = names.Take(take);

			return string.Join (", ", names);
		}
	}
}

