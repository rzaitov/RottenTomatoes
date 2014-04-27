using System;
using System.Collections.Generic;

namespace Logic
{
	public interface IRottenTomatoesService
	{
		void GetTopBoxOfficeAsync(Action<IList<Movie>> callback);

		void GetOpeningThisWeek(Action<IList<Movie>> callback);

		void GetInTheaters(Action<IList<Movie>> callback);
	}
}

