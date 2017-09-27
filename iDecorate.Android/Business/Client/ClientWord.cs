using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using iDecorate.Android.Business.Contract;
using iDecorate.Android.Model;
using System.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace iDecorate.Android.Business.Client
{
    public class ClientWord : IClient<WordModel>
    {
        private string _urlRequest = "http://idecorate.azurewebsites.net/api/Word";
        HttpClient client;

        public ClientWord()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<WordModel> Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WordModel>> GetList()
        {
            var response = await client.GetStringAsync(_urlRequest);
            var todoItems = JsonConvert.DeserializeObject<List<WordModel>>(response);
            return todoItems;
        }

        public async Task<bool> Post(WordModel body)
        {
            var data = JsonConvert.SerializeObject(body);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_urlRequest, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Put(WordModel body)
        {
            var data = JsonConvert.SerializeObject(body);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(_urlRequest, content);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Delete(string id)
        {
            var response = await client.DeleteAsync(string.Concat(_urlRequest, id));

            return response.IsSuccessStatusCode;
        }
    }
}