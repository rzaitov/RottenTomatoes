using System;
using System.Collections.Generic;

namespace Logic
{
	public interface IRottenTomatoesService
	{
		void GetTopBoxOfficeAsync(Action<IList<Movie>> callback);

		void GetOpeningThisWeekAsync(Action<IList<Movie>> callback);

		void GetInTheatersAsync(Action<IList<Movie>> callback);

		void GetMovieDetailsAsync(string movieId, Action<MovieDetails> callback);

		void GetMovieReviewsAsync(string movieId, Action<IList<Review>> callback);

		void GetMovieFullCastAsync(string movieId, Action<IList<Cast>> callback);
	}
}