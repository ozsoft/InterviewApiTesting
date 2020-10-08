using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using ApiTest;
using RestSharp.Authenticators;

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

        public IRestResponse GetResponse(Uri url, RestSharp.Method method)
        {
            var client = new RestClient(url);
            var request = new RestRequest(method);
            var config = new ClientConfig();


            request.AddHeader("Accept", "application/json");

            /*request.AddParameter("clientid", config.client_id, ParameterType.GetOrPost);
            request.AddParameter("key", "value");

            //anonymous body
            request.AddJsonBody(new { name = "Ozgur" });
            */


            return client.Execute(request);

        }


        public IRestResponse GetPostResponse(Uri url, RestSharp.Method method)
        {
            var client = new RestClient(url);

            //client.Authenticator = new SimpleAuthenticator("username", "foo", "password", "bar");
            var AuthToken = "absdasdasdsad";

            var request = new RestRequest(method);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("authorization", "Bearer " + AuthToken);

            return client.Get<List<PostsObject>>(request);

        }

        public List<Dictionary<string, string>> SerialiseResponseToListOfDict(IRestResponse response)
        {
            var deserialiser = new JsonDeserializer();
            var output = deserialiser.Deserialize<List<Dictionary<string, string>>>(response);

            return output;

        }

        public List<CommentsObject> DeserialiseResponseToComments(IRestResponse response)
        {
            var deserialiser = new JsonDeserializer();
            var output = deserialiser.Deserialize<List<CommentsObject>>(response);

            return output;

        }

        public List<PostsObject> DeserialiseResponseToPost(IRestResponse response)
        {
            var deserialiser = new JsonDeserializer();
            var output = deserialiser.Deserialize<List<PostsObject>>(response);

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
