using System;
using System.Net.Http;

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Logic
{
	public class RottenTomatoesService : IRottenTomatoesService
	{
		private const string Key = "chshuwu2c2pu5zhgsv8vrkgd";
		private readonly HttpClient _client;

		private readonly Uri BaseUri = new Uri("http://api.rottentomatoes.com");
		private readonly string TopBoxOfficeResource = "/api/public/v1.0/lists/movies/box_office.json";
		private readonly string OpeningThisWeekResource = "/api/public/v1.0/lists/movies/opening.json";
		private readonly string InTheatersResource = "/api/public/v1.0/lists/movies/in_theaters.json";
		private readonly string KeyQuery;

		public RottenTomatoesService ()
		{
			_client = new HttpClient();
			KeyQuery = string.Format("apikey={0}", Key);
		}

		public void GetTopBoxOfficeAsync(Action<IList<Movie>> callback)
		{
			RequestMovies(TopBoxOfficeResource, callback);
		}

		public void GetOpeningThisWeek(Action<IList<Movie>> callback)
		{
			RequestMovies(OpeningThisWeekResource, callback);
		}

		public void GetInTheaters(Action<IList<Movie>> callback)
		{
			RequestMovies(InTheatersResource, callback);
		}

		private void RequestMovies(string resource, Action<IList<Movie>> callback)
		{
			Uri uri = CreateForResource(resource);

			_client.GetAsync (uri).ContinueWith (requestTask => {
				HttpResponseMessage responce = requestTask.Result;
				responce.EnsureSuccessStatusCode();

				responce.Content.ReadAsStringAsync().ContinueWith(readTask => {
					var strData = readTask.Result;

					MoviesRootObject root = JsonConvert.DeserializeObject<MoviesRootObject>(strData);

					callback(root.movies);
				});
			});
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

