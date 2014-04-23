using System;
using System.Net.Http;

using Newtonsoft.Json;

namespace Logic
{
	public class RottenTomatoesService
	{
		private readonly string TopBoxOffice = "http://api.rottentomatoes.com/api/public/v1.0/lists/movies/box_office.json?apikey=chshuwu2c2pu5zhgsv8vrkgd&limit=1";

		private readonly HttpClient _client;
		private readonly JsonSerializer _serializer;

		public RottenTomatoesService ()
		{
			_client = new HttpClient();
			_serializer = new JsonSerializer ();
		}

		public void GetTopBoxOfficeAsync(Action<string> callback)
		{
			_client.GetAsync (TopBoxOffice).ContinueWith (requestTask => {
				HttpResponseMessage responce = requestTask.Result;
				responce.EnsureSuccessStatusCode();

				responce.Content.ReadAsStringAsync().ContinueWith(readTask => {
					var strData = readTask.Result;

					var ro = JsonConvert.DeserializeObject<RootObject>(strData);
//					var ro = _serializer.Deserialize<RootObject>(new JsonReader());

					callback(strData);
				});
			});
		}
	}
}

