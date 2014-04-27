using System;
using System.Collections.Generic;

namespace Logic
{
	public interface IRottenTomatoesService
	{
		void GetTopBoxOfficeAsync(Action<IList<Movie>> callback);

		void GetOpeningThisWeekAsync(Action<IList<Movie>> callback);

		void GetInTheatersAsync(Action<IList<Movie>> callback);

		void GetMovieDetails(string movieId, Action<MovieDetails> callback);
	}
}

