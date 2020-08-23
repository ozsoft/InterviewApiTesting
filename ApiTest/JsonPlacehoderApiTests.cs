using System;
using System.Net;
using RestSharp;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp.Serialization.Json;
using System.Diagnostics;

namespace Tests
{
    public class JsonPlacehoderApiTests
    {
        string endpoint;

        [SetUp]
        public void Setup()
        {

            endpoint = "https://jsonplaceholder.typicode.com/";

        }

        //Happy paths, positive tests

        [Test]
        public void GetAllPostsHttpsRequestResponseOk()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("posts", Method.GET);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            var deserialiser = new JsonDeserializer();

            var output = deserialiser.Deserialize<List<Dictionary<string, string>>>(response);

            Assert.AreEqual(100, output.Count);


        }

        [Test]
        public void GetAllCommentsHttpsRequestResponseOk()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("comments", Method.GET);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            var deserialiser = new JsonDeserializer();

            var output = deserialiser.Deserialize<List<Dictionary<string, string>>>(response);

            Assert.AreEqual(500, output.Count);


        }

        [Test]
        public void GetCommentsByIdHttpsRequestResponseOk()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("comments/1", Method.GET);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            var deserialiser = new JsonDeserializer();

            var output = deserialiser.Deserialize<List<Dictionary<string, string>>>(response);

            var postId = output[0]["postId"];
            var id = output[0]["id"];
            var name = output[0]["name"];
            var email = output[0]["email"];
            var body = output[0]["body"];


            Assert.AreEqual("1", postId);
            Assert.AreEqual("1", id);
            Assert.AreEqual("id labore ex et quam laborum", name);
            Assert.AreEqual("Eliseo@gardner.biz", email);
            Assert.AreEqual("laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora quo necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium", body);

            Assert.AreEqual(1, output.Count);


        }




        [Test]
        public void GetPostsByUserHttpsRequestResponseOk()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("posts?userId=1", Method.GET);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            var deserialiser = new JsonDeserializer();

            var output = deserialiser.Deserialize<List<Dictionary<string, string>>>(response);

            Assert.AreEqual(10, output.Count);


        }

        [Test]
        public void GetCommentsByPostIdHttpsRequestResponseOk()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("/comments?postId=1", Method.GET);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(statusCode, HttpStatusCode.OK);
            var deserialiser = new JsonDeserializer();

            var output = deserialiser.Deserialize<List<Dictionary<string, string>>>(response);

            Assert.AreEqual(5, output.Count);

        }


        
        [Test]
        public void GetCommentsByPostSecondUrlIdHttpsRequestResponseOk()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("posts/1/comments", Method.GET);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(statusCode, HttpStatusCode.OK);
            var deserialiser = new JsonDeserializer();

            var output = deserialiser.Deserialize<List<Dictionary<string, string>>>(response);

            Assert.AreEqual(5, output.Count);

            var postId = output[0]["postId"];
            var id = output[0]["id"];
            var name = output[0]["name"];
            var email = output[0]["email"];
            var body = output[0]["body"];


            Assert.AreEqual("1", postId);
            Assert.AreEqual("1", id);
            Assert.AreEqual("id labore ex et quam laborum", name);
            Assert.AreEqual("Eliseo@gardner.biz", email);
            Assert.AreEqual("laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora quo necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium", body);


        }

        [Test]
        public void GetPostsHttpsRequestResponseOk()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            var client = new RestClient(endpoint);

            var request = new RestRequest("posts/2", Method.GET);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            string contentType = response.ContentType;


            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            Assert.AreEqual("application/json; charset=utf-8", contentType);

            var deserialiser = new JsonDeserializer();
            var output = deserialiser.Deserialize<Dictionary<string, string>>(response);

            stopwatch.Stop();


            //if the response is more than 5 seconds we should fail, as this is way too long for any API response for a single post
            if(stopwatch.Elapsed.Seconds > 5)
            {
                Assert.Fail("Response took more than 5 seconds");
            }




            var userId = output["userId"];
            var id = output["id"];
            var title = output["title"];
            var body = output["body"];

            Assert.AreEqual("1", userId);
            Assert.AreEqual("2", id);
            Assert.AreEqual("qui est esse", title);
            Assert.AreEqual("est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla", body);


        }

        [Test]
        public void PostPostsHttpsRequestResponseOk()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("posts", Method.POST);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.Created, statusCode);
            


        }

        [Test]
        public void PutPostsHttpsRequestResponseOk()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("posts/1", Method.PUT);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);



        }

        [Test]
        public void PatchPostsHttpsRequestResponseOk()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("posts/1", Method.PATCH);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);



        }

        [Test]
        public void DeletePostsHttpsRequestResponseOk()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("posts/1", Method.DELETE);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);



        }

        [Test]
        public void DeleteNonExistingPostsHttpsRequestResponseOk()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("posts/1000", Method.DELETE);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;

            
            //This returns an OK and there is no way of telling if the post has been deleted or not
            Assert.AreEqual(HttpStatusCode.OK, statusCode);



        }



        //HTTP (not HTTPS) test here
        [Test]
        public void GetPostHttpRequestResponseOk()
        {
            var client = new RestClient("http://jsonplaceholder.typicode.com/");

            var request = new RestRequest("posts/2", Method.GET);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(statusCode, HttpStatusCode.OK);
            var deserialiser = new JsonDeserializer();

            var output = deserialiser.Deserialize<Dictionary<string, string>>(response);

            var userId = output["userId"];
            var id = output["id"];
            var title = output["title"];
            var body = output["body"];

            Assert.AreEqual("1", userId);
            Assert.AreEqual("2", id);
            Assert.AreEqual("qui est esse", title);
            Assert.AreEqual("est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla", body);



        }




        /// 
        /// NEGATIVE TESTS
        /// 



        // Negative test, return not found
        [Test]
        public void PostHttpsRequestResponseNotFound()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("posts/-1", Method.GET);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode);

        }

        [Test]
        public void GetPostWitIncorrectRequestHttpsRequestResponseNotFound()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("post/1", Method.GET);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode);

        }

        [Test]
        public void OptionsPostHttpsRequestResponseNotFound()
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("posts/1", Method.OPTIONS);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;

            //HTTP Method OPTIONS is not supported should return nocontent
            Assert.AreEqual(HttpStatusCode.NoContent, statusCode);

        }


        [Test]
        public void HeadPostsHttpsRequestResponseOk()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            var client = new RestClient(endpoint);

            var request = new RestRequest("posts/2", Method.HEAD);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            var deserialiser = new JsonDeserializer();


            //Head returns no content
            Assert.AreEqual("", response.Content);

        }



        //Test Headers



    }
}