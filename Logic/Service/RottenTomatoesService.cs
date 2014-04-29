using System;
using System.Net.Http;

using Newtonsoft.Json;
using System.Collections.Generic;

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

		public void GetTopBoxOfficeAsync(Action<IList<Movie>> callback)
		{
			RequestMoviesList(TopBoxOfficeResource, callback);
		}

		public void GetOpeningThisWeekAsync(Action<IList<Movie>> callback)
		{
			RequestMoviesList(OpeningThisWeekResource, callback);
		}

		public void GetInTheatersAsync(Action<IList<Movie>> callback)
		{
			RequestMoviesList(InTheatersResource, callback);
		}

		private void RequestMoviesList(string resource, Action<IList<Movie>> callback)
		{
			Uri uri = CreateForResource(resource);
			ExecuteAsync<MoviesRootObject>(uri, root => callback(root.movies));
		}

		public void GetMovieDetailsAsync(string movieId, Action<MovieDetails> callback)
		{
			string resource = string.Format(MovieDetailsResourcePattern, movieId);
			Uri movieDetailsUri = CreateForResource(resource);
			ExecuteAsync<MovieDetails>(movieDetailsUri, callback);
		}

		public void GetMovieReviewsAsync(string movieId, Action<IList<Review>> callback)
		{
			string resource = string.Format(MovieReviesResourcePattern, movieId);
			Uri movieReviewsUri = CreateForResource(resource);
			ExecuteAsync<CriticsRootObject>(movieReviewsUri, cro => callback(cro.reviews));
		}

		public void GetMovieFullCastAsync(string movieId, Action<IList<Cast>> callback)
		{
			string resource = string.Format(MovieFullCastPattern, movieId);
			Uri movieFullCastUri = CreateForResource(resource);
			ExecuteAsync<CastRootObject>(movieFullCastUri, cro => callback(cro.cast));
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

