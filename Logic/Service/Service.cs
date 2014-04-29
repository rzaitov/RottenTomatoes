using System;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Logic
{
	public class Service
	{
		protected HttpClient Client { get; private set; }

		public Service()
		{
			Client = new HttpClient();
		}

		public void ExecuteAsync<T>(Uri uri, Action<T> callback)
		{
			Client.GetAsync (uri).ContinueWith (requestTask => {
				if(requestTask.Status != TaskStatus.RanToCompletion)
					return;

				HttpResponseMessage responce = requestTask.Result;
				responce.EnsureSuccessStatusCode();

				responce.Content.ReadAsStringAsync().ContinueWith(readTask => {
					var strData = readTask.Result;

					T root = JsonConvert.DeserializeObject<T>(strData);

					callback(root);
				});
			});
		}
	}
}

