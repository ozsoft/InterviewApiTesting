using System;
using System.Net;
using RestSharp;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp.Serialization.Json;

namespace Tests
{
    public class Tests
    {
        string endpoint;
         
        [SetUp]
        public void Setup()
        {

            endpoint = "https://jsonplaceholder.typicode.com/";

        }

        [Test]
        public void RequestOk()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("posts/2", Method.GET);
            //request.AddUrlSegment("{postid}", 1);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode; 
            Assert.AreEqual(statusCode, HttpStatusCode.OK);
            var deserialiser = new JsonDeserializer();

            var output = deserialiser.Deserialize<Dictionary<string, string>>(response);
            var r = output["title"];
            Assert.AreEqual("qui est esse", r);


        }


        [Test]
        public void RequestWithHttp()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com/");

            var request = new RestRequest("posts/2", Method.GET);
            //request.AddUrlSegment("{postid}", 1);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(statusCode, HttpStatusCode.OK);
            var deserialiser = new JsonDeserializer();

            var output = deserialiser.Deserialize<Dictionary<string, string>>(response);
            var r = output["title"];
            Assert.AreEqual("qui est esse", r);


        }

        [Test]
        public void CommentPostIdRequest()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com/");

            var request = new RestRequest("/comments?postId=1", Method.GET);
            //request.AddUrlSegment("{postid}", 1);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(statusCode, HttpStatusCode.OK);
            var deserialiser = new JsonDeserializer();

            var output = deserialiser.Deserialize<List<Dictionary<string, string>>>(response);
            var r = output[0]["name"];

            Assert.AreEqual("id labore ex et quam laborum", r);


        }
    }
}