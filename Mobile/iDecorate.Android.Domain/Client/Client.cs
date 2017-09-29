using iDecorate.Android.Domain.Contract;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace iDecorate.Android.Domain.Client
{
    public class Client<T> : IClient<T>
    {
        private string _urlRequest = "http://idecorate.azurewebsites.net/api/";
        HttpClient client;

        public Client(string call)
        {
            _urlRequest += call;
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<T> Get(string id)
        {
            var response = await client.GetStringAsync(string.Concat(_urlRequest, "/", id));
            var result = JsonConvert.DeserializeObject<T>(response);
            return result;
        }

        public async Task<IEnumerable<T>> GetList()
        {
            var response = await client.GetStringAsync(_urlRequest);
            var result = JsonConvert.DeserializeObject<List<T>>(response);
            return result;
        }

        public async Task<bool> Post(T body)
        {
            var data = JsonConvert.SerializeObject(body);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_urlRequest, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Put(T body)
        {
            var data = JsonConvert.SerializeObject(body);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(_urlRequest, content);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Delete(string id)
        {
            var response = await client.DeleteAsync(string.Concat(_urlRequest, "/", id));
            return response.IsSuccessStatusCode;
        }
    }
}