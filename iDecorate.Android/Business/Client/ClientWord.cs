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

namespace iDecorate.Android.Business.Client
{
    public class ClientWord : IClient<WordModel>
    {
        private string _urlRequest = "http://idecorate.azurewebsites.net/api/Word";

        public async Task<IEnumerable<WordModel>> GetList()
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(_urlRequest));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));

                    var result = JsonConvert.DeserializeObject<IEnumerable<WordModel>>(jsonDoc.ToString());

                    // Return the JSON document:
                    return result;
                }
            }
        }

        public void Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Post(WordModel body)
        {
            throw new NotImplementedException();
        }

        public void Put(WordModel body)
        {
            throw new NotImplementedException();
        }

        public Task<WordModel> Get()
        {
            throw new NotImplementedException();
        }
    }
}