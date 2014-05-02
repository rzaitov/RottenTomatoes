using System;
using System.Net.Http;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic
{
	public class RottenTomatoesService : Service, IRottenTomatoesService
	{
		private readonly string Key;
		private readonly Uri BaseUri = new Uri("http://api.rottentomatoes.com");

		private readonly string TopBoxOfficeResource =    "/api/public/v1.0/lists/movies/box_office.json";
		private readonly string OpeningThisWeekResource = "/api/public/v1.0/lists/movies/opening.json";
		private readonly string InTheatersResource =      "/api/public/v1.0/lists/movies/in_theaters.json";

		private readonly string MovieDetailsResourcePattern = "/api/public/v1.0/movies/{0}.json";
		private readonly string MovieReviesResourcePattern =  "/api/public/v1.0/movies/{0}/reviews.json";
		private readonly string MovieFullCastPattern =        "/api/public/v1.0/movies/{0}/cast.json";

		private readonly string KeyQuery;

		public RottenTomatoesService (string key)
		{
			Key = key;
			KeyQuery = string.Format("apikey={0}", Key);
		}

		public Task<IList<Movie>> GetTopBoxOfficeAsync()
		{
			return RequestMoviesList(TopBoxOfficeResource);
		}

		public Task<IList<Movie>> GetOpeningThisWeekAsync()
		{
			return RequestMoviesList(OpeningThisWeekResource);
		}

		public Task<IList<Movie>> GetInTheatersAsync()
		{
			return RequestMoviesList(InTheatersResource);
		}

		private Task<IList<Movie>> RequestMoviesList(string resource)
		{
			Uri uri = CreateForResource(resource);

			return ExecuteAsync<MoviesRootObject>(uri).ContinueWith(t => {
				return (IList<Movie>)t.Result.movies;
			});
		}

		public Task<MovieDetails> GetMovieDetailsAsync(string movieId)
		{
			string resource = string.Format(MovieDetailsResourcePattern, movieId);
			Uri movieDetailsUri = CreateForResource(resource);

			return ExecuteAsync<MovieDetails>(movieDetailsUri);
		}

		public Task<IList<Review>> GetMovieReviewsAsync(string movieId)
		{
			string resource = string.Format(MovieReviesResourcePattern, movieId);
			Uri movieReviewsUri = CreateForResource(resource);

			return ExecuteAsync<CriticsRootObject>(movieReviewsUri).ContinueWith(t => {
				return (IList<Review>)t.Result.reviews;
			});
		}

		public Task<IList<Cast>> GetMovieFullCastAsync(string movieId)
		{
			string resource = string.Format(MovieFullCastPattern, movieId);
			Uri movieFullCastUri = CreateForResource(resource);

			return ExecuteAsync<CastRootObject>(movieFullCastUri).ContinueWith(t => {
				return (IList<Cast>)t.Result.cast;
			});
		}

		public void GetMovieFullDetailsAsync(string movieId, Action callback)
		{

		}

		private Uri CreateForResource(string resource)
		{
			UriBuilder builder = new UriBuilder(BaseUri);
			builder.Path = resource;
			builder.Query = KeyQuery;

			return builder.Uri;
		}
	}
}

