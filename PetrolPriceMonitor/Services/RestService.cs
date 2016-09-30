using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.Services
{
    public class RestService : IConsume
    {
        private HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 256000;
        }

        async public Task<T> GetAsync<T>(string url, JsonSerializerSettings settings = null)
        { 
            var uri = new Uri(string.Format(url, string.Empty));

            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (settings == null)
                    return JsonConvert.DeserializeObject<T>(content, settings);
                else
                    return JsonConvert.DeserializeObject<T>(content, settings);
            }
            else
            {
                return default(T);
            }
        }
    }
}
