using System;
using System.Net;
using RestSharp;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp.Serialization.Json;
using System.Diagnostics;
using ApiTest;

namespace Tests
{
    public class JsonPlacehoderApiTests
    {
        ApiHelper api = new ApiHelper(new Uri("https://jsonplaceholder.typicode.com/"));


        [SetUp]
        public void Setup()
        {

        }


        //Happy paths, positive tests

        [Test]
        public void GetAllPostsHttpsRequestResponseOk()
        {
            //set url using API helper
            Uri url = api.SetUrl("posts");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.GET);

            //deserialise the response content and put into a List of Post object
            var output = api.DeserialiseResponse<List<PostsObject>>(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);
            var responseInt = (int)response.StatusCode;
            Assert.AreEqual(200, responseInt);

            //assert that the size of output from response is as expected
            Assert.AreEqual(100, output.Count);


        }

        [Test]
        public void GetAllCommentsHttpsRequestResponseOk()
        {
            //set url using API helper
            Uri url = api.SetUrl("comments");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.DeserialiseResponse<List<CommentsObject>>(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


            //assert that the size of output from response is as expected
            Assert.AreEqual(500, output.Count);

        }

        [Test]
        public void GetCommentsByIdHttpsRequestResponseOk()
        {

            //set url using API helper
            Uri url = api.SetUrl("comments/1");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.DeserialiseResponse<List<CommentsObject>>(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


            var postId = output[0].postId;
            var id = output[0].id;
            var name = output[0].name;
            var email = output[0].email;
            var body = output[0].body;


            Assert.AreEqual(1, postId);
            Assert.AreEqual(1, id);
            Assert.AreEqual("id labore ex et quam laborum", name);
            Assert.AreEqual("Eliseo@gardner.biz", email);
            Assert.AreEqual("laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora quo necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium", body);

            Assert.AreEqual(1, output.Count);


        }




        [Test]
        public void GetPostsByUserHttpsRequestResponseOk()
        {

            //set url using API helper
            Uri url = api.SetUrl("posts?userId=1");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.DeserialiseResponse<List<PostsObject>>(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


            //assert that the size of output from response is as expected

            Assert.AreEqual(10, output.Count);


        }

        [Test]
        public void GetCommentsByPostIdHttpsRequestResponseOk()
        {

            //set url using API helper
            Uri url = api.SetUrl("comments?postId=1");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.DeserialiseResponse<List<CommentsObject>>(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


            Assert.AreEqual(5, output.Count);

        }



        [Test]
        public void GetCommentsByPostSecondUrlIdHttpsRequestResponseOk()
        {

            //set url using API helper
            Uri url = api.SetUrl("posts/1/comments");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.DeserialiseResponse<List<CommentsObject>>(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


            //Confirm the size of the results from response is 5 by using the lists size
            Assert.AreEqual(5, output.Count);


            //check the first results content i.e. postid=1 
            var postId = output[0].postId;
            var id = output[0].id;
            var name = output[0].name;
            var email = output[0].email;
            var body = output[0].body;



            //Asserts of the content of the first result
            Assert.AreEqual(1, postId);
            Assert.AreEqual(1, id);
            Assert.AreEqual("id labore ex et quam laborum", name);
            Assert.AreEqual("Eliseo@gardner.biz", email);
            Assert.AreEqual("laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora quo necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium", body);


        }

        [Test]
        public void GetPostsHttpsRequestResponseOkAndTestPerformanceOfApi()
        {
            //start a stopwatch to time the api call
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            //set url using API helper
            Uri url = api.SetUrl("posts/2");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.DeserialiseResponse<List<PostsObject>>(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


            stopwatch.Stop();


            //if the response is more than 5 seconds we should fail, as this is way too long for any API response for a single post
            if (stopwatch.Elapsed.Seconds > 5)
            {
                Assert.Fail("Response took more than 5 seconds");
            }


            var userId = output[0].userId;
            var id = output[0].id;
            var title = output[0].title;
            var body = output[0].body;

            Assert.AreEqual(1, userId);
            Assert.AreEqual(2, id);
            Assert.AreEqual("qui est esse", title);
            Assert.AreEqual("est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla", body);


        }

        [Test]
        public void PostPostsHttpsRequestResponseOk()
        {

            //set url using API helper
            Uri url = api.SetUrl("posts/");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.POST);

            //serialise the response content and put into a List of dictionary
            var output = api.DeserialiseResponse<List<PostsObject>>(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.Created, response.StatusCode);


        }

        [Test]
        public void PutPostsHttpsRequestResponseOk()
        {



            //set url using API helper
            Uri url = api.SetUrl("posts/1");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.PUT);

            //serialise the response content and put into a List of dictionary
            var output = api.DeserialiseResponse<List<PostsObject>>(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);



        }

        [Test]
        public void PatchPostsHttpsRequestResponseOk()
        {
            //set url using API helper
            Uri url = api.SetUrl("posts/1");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.PATCH);

            //serialise the response content and put into a List of dictionary
            var output = api.DeserialiseResponse<List<PostsObject>>(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


        }

        [Test]
        public void DeletePostsHttpsRequestResponseOk()
        {

            //set url using API helper
            Uri url = api.SetUrl("posts/1");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.DELETE);

            //serialise the response content and put into a List of dictionary
            var output = api.DeserialiseResponse<List<PostsObject>>(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);

        }

        [Test]
        public void DeleteNonExistingPostsHttpsRequestResponseOk()
        {

            //set url using API helper
            Uri url = api.SetUrl("posts/1000");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.DELETE);

            //serialise the response content and put into a List of dictionary
            var output = api.DeserialiseResponse<List<PostsObject>>(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


        }



        //HTTP (not HTTPS) test here
        [Test]
        public void GetPostHttpRequestResponseOk()
        {

            ApiHelper apiHttp = new ApiHelper(new Uri("http://jsonplaceholder.typicode.com/"));

            //set url using API helper
            Uri url = apiHttp.SetUrl("posts/2");

            //get response back by calling API as a GET request
            var response = apiHttp.GetResponse(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = apiHttp.DeserialiseResponse<List<PostsObject>>(response);


            //Assert that expected http code matches the response http code 
            apiHttp.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);

            //check the posts/2 returned values
            var userId = output[0].userId;
            var id = output[0].id;
            var title = output[0].title;
            var body = output[0].body;


            Assert.AreEqual(1, userId);
            Assert.AreEqual(2, id);
            Assert.AreEqual("qui est esse", title);
            Assert.AreEqual("est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla", body);



        }




        /// 
        /// NEGATIVE TESTS
        /// 



        // Negative test, return not found
        [Test]
        public void GetPostHttpsRequestResponseNotFound()
        {

            //set url using API helper
            Uri url = api.SetUrl("posts/-1");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.GET);

            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Test]
        public void GetPostWitIncorrectRequestHttpsRequestResponseNotFound()
        {
            //incorrect url
            Uri url = api.SetUrl("pos/1");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.GET);

            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Test]
        public void OptionsPostHttpsRequestResponseNotFound()
        {

            Uri url = api.SetUrl("posts/1");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.OPTIONS);

            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.NoContent, response.StatusCode);
        }


        [Test]
        public void HeadPostsHttpsRequestResponseOk()
        {


            //set url using API helper
            Uri url = api.SetUrl("posts/2");

            //get response back by calling API as a GET request
            var response = api.GetResponse(url, Method.HEAD);

            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


            //Head returns no content
            Assert.AreEqual("", response.Content);


        }






    }
}