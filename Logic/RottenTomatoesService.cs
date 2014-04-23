using System;
using System.Net.Http;

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Logic
{
	public class RottenTomatoesService
	{
		private readonly string TopBoxOffice = "http://api.rottentomatoes.com/api/public/v1.0/lists/movies/box_office.json?apikey=chshuwu2c2pu5zhgsv8vrkgd";

		private readonly HttpClient _client;

		public RottenTomatoesService ()
		{
			_client = new HttpClient();
		}

		public void GetTopBoxOfficeAsync(Action<IList<Movie>> callback)
		{
			_client.GetAsync (TopBoxOffice).ContinueWith (requestTask => {
				HttpResponseMessage responce = requestTask.Result;
				responce.EnsureSuccessStatusCode();

				responce.Content.ReadAsStringAsync().ContinueWith(readTask => {
					var strData = readTask.Result;

					RootObjectTopBoxOffice root = JsonConvert.DeserializeObject<RootObjectTopBoxOffice>(strData);

					callback(root.movies);
				});
			});
		}
	}
}

