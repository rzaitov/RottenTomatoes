using System;
using System.Net.Http;
using System.Threading;
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

//		public void ExecuteAsync<T>(Uri uri, Action<T> callback)
//		{
//			Client.GetAsync(uri).ContinueWith(requestTask => {
//				if (requestTask.Status != TaskStatus.RanToCompletion)
//					return;
//
//				HttpResponseMessage responce = requestTask.Result;
//				responce.EnsureSuccessStatusCode();
//
//				responce.Content.ReadAsStringAsync().ContinueWith(readTask => {
//					var strData = readTask.Result;
//
//					T root = JsonConvert.DeserializeObject<T>(strData);
//
//					callback(root);
//				});
//			});
//		}

		public Task<T> ExecuteAsync<T>(Uri uri)
		{
			var requestTask = Client.GetAsync(uri);

			var readResponseTask = requestTask.ContinueWith(rt => {
				HttpResponseMessage responce = rt.Result;
				responce.EnsureSuccessStatusCode();

				Task<string> readTask = responce.Content.ReadAsStringAsync();
				return readTask.Result;
			}, TaskContinuationOptions.OnlyOnRanToCompletion);

			var deserializeTask = readResponseTask.ContinueWith(rrt => {
				T root = JsonConvert.DeserializeObject<T>(rrt.Result);
				return root;
			});

			return deserializeTask;
		}
	}
}

