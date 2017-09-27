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
    public class ClientTopic : IClient<TopicModel>
    {
        private string _urlRequest = "http://idecorate.azurewebsites.net/api/Topic";

        public async Task<IEnumerable<TopicModel>> GetList()
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

                    var result = JsonConvert.DeserializeObject<IEnumerable<TopicModel>>(jsonDoc.ToString());

                    // Return the JSON document:
                    return result.ToList();
                }
            }
        }

        public void Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Post(TopicModel body)
        {
            string poststring = String.Format("description={0}", body.description);

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(_urlRequest);

            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";

            byte[] bytedata = Encoding.UTF8.GetBytes(poststring);
            httpRequest.ContentLength = bytedata.Length;

            Stream requestStream = httpRequest.GetRequestStream();
            requestStream.Write(bytedata, 0, bytedata.Length);
            requestStream.Close();

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();

            StringBuilder sb = new StringBuilder();

            using (StreamReader reader =
            new StreamReader(responseStream, System.Text.Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sb.Append(line);
                }
            }
        }

        public void Put(TopicModel body)
        {
            throw new NotImplementedException();
        }

        public Task<TopicModel> Get()
        {
            throw new NotImplementedException();
        }
    }
}