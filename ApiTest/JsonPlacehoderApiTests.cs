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
        //this will create an ApiHelper so we can utilise in most of the test below
        ApiHelper api = new ApiHelper(new Uri("https://jsonplaceholder.typicode.com/"));


        //Unit setup method, not used for the moment, but will become more useful as the framework matures
        //we can insert anything we need processing prior to any tests been run here
        [SetUp]
        public void Setup()
        {



        }


        //Happy paths, positive tests

        [Test]
        public void GetAllPostsHttpsRequestResponseOk()
        {
            //set url using API helper, will use prefix of url above and the below postfix and concatenate the full URL to call
            Uri url = api.SetUrl("posts");

            //get response back by calling API as a GET request
            var response = api.CreateRequest(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.SerialiseResponseToListOfDict(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


            //assert that the size of output from response is as expected
            //this is a basic tests that the returned number of responses are matching what we expect
            Assert.AreEqual(100, output.Count);


        }

        [Test]
        public void GetAllCommentsHttpsRequestResponseOk()
        {
            //set url using API helper
            Uri url = api.SetUrl("comments");

            //get response back by calling API as a GET request
            var response = api.CreateRequest(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.SerialiseResponseToListOfDict(response);


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
            var response = api.CreateRequest(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.SerialiseResponseToListOfDict(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


            //use the data in output which is in a List of dictionary and assert the values and keys for the first returned object
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


            //check that the list of dictionary has exactly 1 count as we have specifically asked for a comments with a specific id 
            Assert.AreEqual(1, output.Count);


        }




        [Test]
        public void GetPostsByUserHttpsRequestResponseOk()
        {

            //set url using API helper
            Uri url = api.SetUrl("posts?userId=1");

            //get response back by calling API as a GET request
            var response = api.CreateRequest(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.SerialiseResponseToListOfDict(response);


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
            var response = api.CreateRequest(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.SerialiseResponseToListOfDict(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);

            //assert that the size of output from response is as expected
            Assert.AreEqual(5, output.Count);

        }



        [Test]
        public void GetCommentsByPostSecondUrlIdHttpsRequestResponseOk()
        {

            //set url using API helper
            Uri url = api.SetUrl("posts/1/comments");

            //get response back by calling API as a GET request
            var response = api.CreateRequest(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.SerialiseResponseToListOfDict(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


            //Confirm the size of the results from response is 5 by using the lists size
            Assert.AreEqual(5, output.Count);


            //check the first results content i.e. postid=1 
            var postId = output[0]["postId"];
            var id = output[0]["id"];
            var name = output[0]["name"];
            var email = output[0]["email"];
            var body = output[0]["body"];


            //Asserts of the content of the first result
            Assert.AreEqual("1", postId);
            Assert.AreEqual("1", id);
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
            var response = api.CreateRequest(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = api.SerialiseResponseToDictionary(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


            stopwatch.Stop();


            //if the response is more than 5 seconds we should fail, as this is way too long for any API response for a single post
            if (stopwatch.Elapsed.Seconds > 5)
            {
                Assert.Fail("Response took more than 5 seconds");
            }


            //check the fields and values for post with id=2 is correct
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

            //set url using API helper
            Uri url = api.SetUrl("posts/");

            //get response back by calling API as a POST request
            var response = api.CreateRequest(url, Method.POST);

            //serialise the response content and put into a List of dictionary
            var output = api.SerialiseResponseToListOfDict(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.Created, response.StatusCode);


        }

        [Test]
        public void PutPostsHttpsRequestResponseOk()
        {



            //set url using API helper
            Uri url = api.SetUrl("posts/1");

            //get response back by calling API as a PUT request
            var response = api.CreateRequest(url, Method.PUT);

            //serialise the response content and put into a List of dictionary
            var output = api.SerialiseResponseToListOfDict(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);



        }

        [Test]
        public void PatchPostsHttpsRequestResponseOk()
        {
            //set url using API helper
            Uri url = api.SetUrl("posts/1");

            //get response back by calling API as a PATCH request
            var response = api.CreateRequest(url, Method.PATCH);

            //serialise the response content and put into a List of dictionary
            var output = api.SerialiseResponseToListOfDict(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


        }

        [Test]
        public void DeletePostsHttpsRequestResponseOk()
        {

            //set url using API helper
            Uri url = api.SetUrl("posts/1");

            //get response back by calling API as a DELETE request
            var response = api.CreateRequest(url, Method.DELETE);

            //serialise the response content and put into a List of dictionary
            var output = api.SerialiseResponseToListOfDict(response);


            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);

        }

        [Test]
        public void DeleteNonExistingPostsHttpsRequestResponseOk()
        {

            //set url using API helper
            Uri url = api.SetUrl("posts/1000");

            //get response back by calling API as a DELETE request
            var response = api.CreateRequest(url, Method.DELETE);

            //serialise the response content and put into a List of dictionary
            var output = api.SerialiseResponseToListOfDict(response);


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
            var response = apiHttp.CreateRequest(url, Method.GET);

            //serialise the response content and put into a List of dictionary
            var output = apiHttp.SerialiseResponseToDictionary(response);


            //Assert that expected http code matches the response http code 
            apiHttp.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);

            //check the posts/2 returned values
            var userId = output["userId"];
            var id = output["id"];
            var title = output["title"];
            var body = output["body"];

            Assert.AreEqual("1", userId);
            Assert.AreEqual("2", id);
            Assert.AreEqual("qui est esse", title);
            Assert.AreEqual("est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla", body);



        }




        // Negative tests

        [Test]
        public void GetPostHttpsRequestResponseNotFound()
        {

            //set url using API helper
            Uri url = api.SetUrl("posts/-1");

            //get response back by calling API as a GET request
            var response = api.CreateRequest(url, Method.GET);

            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Test]
        public void GetPostWitIncorrectRequestHttpsRequestResponseNotFound()
        {
            //incorrect url
            Uri url = api.SetUrl("pos/1");

            //get response back by calling API as a GET request
            var response = api.CreateRequest(url, Method.GET);

            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Test]
        public void OptionsPostHttpsRequestResponseNotFound()
        {

            Uri url = api.SetUrl("posts/1");

            //get response back by calling API as a OPTIONS request
            var response = api.CreateRequest(url, Method.OPTIONS);

            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.NoContent, response.StatusCode);
        }


        [Test]
        public void HeadPostsHttpsRequestResponseOk()
        {


            //set url using API helper
            Uri url = api.SetUrl("posts/2");

            //get response back by calling API as a HEAD request
            var response = api.CreateRequest(url, Method.HEAD);

            //Assert that expected http code matches the response http code 
            api.CheckHttpCodeResponse(HttpStatusCode.OK, response.StatusCode);


            //Head returns no content
            Assert.AreEqual("", response.Content);


        }






    }
}