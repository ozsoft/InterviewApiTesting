using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;

namespace ApiTest
{
    public class ApiHelper
    {
        public Uri baseUrl;

        public ApiHelper(Uri url)
        {
            this.baseUrl = url;


        }
        public RestClient restClient;
        public RestRequest restRequest;


        public Uri SetUrl(string endpoint)
        {
            string url = Path.Combine(baseUrl.ToString(), endpoint);
            Uri myUri = new Uri(url);

            return myUri;
        }

        public IRestResponse CreateRequest(Uri url, RestSharp.Method method)
        {
            var client = new RestClient(url);

            var request = new RestRequest(method);
            var response = client.Execute(request);

            return response;

        }

        public List<Dictionary<string, string>> SerialiseResponseToListOfDict(IRestResponse response)
        {
            var deserialiser = new JsonDeserializer();
            var output = deserialiser.Deserialize<List<Dictionary<string, string>>>(response);

            return output;

        }

        public Dictionary<string, string> SerialiseResponseToDictionary(IRestResponse response)
        {
            var deserialiser = new JsonDeserializer();
            var output = deserialiser.Deserialize<Dictionary<string, string>>(response);

            return output;

        }


        public void CheckHttpCodeResponse(HttpStatusCode expectedCode, HttpStatusCode actualCode)
        {

            Assert.AreEqual(expectedCode, actualCode);
        }

    }
}
