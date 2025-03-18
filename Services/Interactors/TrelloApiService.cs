using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Services.Boundaries;
using Services.Models.Trello;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interactors
{
    public class TrelloApiService : ITrelloApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        private readonly TrelloSection _settings;

        public TrelloApiService(IOptions<TrelloSection> trelloOptions, IHttpClientFactory httpClientFactory)
        {
            _settings = trelloOptions.Value;
			_httpClientFactory = httpClientFactory;
		}

        public async Task<T> GetDataAsync<T>(string url, Dictionary<string, string> headers = null)
        {
            using (var client = GetClient())
            {
                if (headers != null)
                {
                    foreach (var key in headers.Keys)
                    {
                        client.DefaultRequestHeaders.Add(key, headers[key]);
                    }
                }

                var apiUrl = $"{_settings.ApiBaseUrl}/{url}?key={_settings.ApiKey}&token={_settings.Token}";

                var response = await client.GetAsync(url).ConfigureAwait(continueOnCapturedContext: false);
                ValidateResponse(url, nameof(GetDataAsync), response);
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task DeleteDataAsync(string url, Dictionary<string, string> headers = null)
        {
            using (var client = GetClient())
            {
                if (headers != null)
                {
                    foreach (var key in headers.Keys)
                    {
                        client.DefaultRequestHeaders.Add(key, headers[key]);
                    }
                }

                var response = await client.DeleteAsync(url).ConfigureAwait(continueOnCapturedContext: false);
                ValidateResponse(url, nameof(DeleteDataAsync), response);
                
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                //return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task PostDataAsync(string url, object data = null, Dictionary<string, string> headers = null)
        {
            using (var client = GetClient())
            {
                if (headers != null)
                {
                    foreach (var key in headers.Keys)
                    {
                        client.DefaultRequestHeaders.Add(key, headers[key]);
                    }
                }

				StringContent content = null;
                if (data != null)
                    content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content).ConfigureAwait(continueOnCapturedContext: false);
                ValidateResponse(url, nameof(PostDataAsync), response);

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);

                //return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task PutDataAsync(string url, object data = null, Dictionary<string, string> headers = null)
        {
            using (var client = GetClient())
            {
                if (headers != null)
                {
                    foreach (var key in headers.Keys)
                    {
                        client.DefaultRequestHeaders.Add(key, headers[key]);
                    }
                }

                StringContent content = null;
                if (data != null)
                    content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                var response = await client.PutAsync(url, content).ConfigureAwait(continueOnCapturedContext: false);
                ValidateResponse(url, nameof(PutDataAsync), response);

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);

                //return JsonConvert.DeserializeObject<T>(json);
            }
        }

        private static void ValidateResponse(string url, string method, HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"{nameof(TrelloApiService)}.{method} -> {url} : status code {response.StatusCode}");
            }
        }

        private HttpClient GetClient()
        {
            return _httpClientFactory.CreateClient("trello");
		}
	}
}
