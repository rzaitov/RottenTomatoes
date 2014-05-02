using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic
{
	public interface IRottenTomatoesService
	{
		Task<IList<Movie>> GetTopBoxOfficeAsync();

		Task<IList<Movie>> GetOpeningThisWeekAsync();

		Task<IList<Movie>> GetInTheatersAsync();

		Task<MovieDetails> GetMovieDetailsAsync(string movieId);

		Task<IList<Review>> GetMovieReviewsAsync(string movieId);

		Task<IList<Cast>> GetMovieFullCastAsync(string movieId);
	}
}