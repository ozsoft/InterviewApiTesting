﻿
-----INTRODUCTION-----
This is the document to outline the findings from testing JSON Placeholder API


-----FRAMEWORK DEPENDENCIES-----
Focused on the following endpoints:
	/posts
	/posts/1
	/posts/1/comments
	/comments?postId=1
	/posts?userId=1

with the prefix endpoint as : https://jsonplaceholder.typicode.com

I have used the following to develop the project:
Visual Studio 2017 on Windows 7
Visual Studio for Mac 8.7.3 (cloned from github)
C# Language
Developed in .NET Core 2.1
NUnit application solution/project

Dependencies required to run tests: 
NUnit (3.10.1)
NUnit3TestAdapter (3.10.0)
RestSharp (106.11.4)
Microsoft.NET.Test.Sdk (15.8.0)


-----INTRUCTIONS TO RUN-----
Use the GitHub code link here and clone to your local: https://github.com/ozsoft/InterviewApiTesting.git
Check dependencies shown above and install all in your IDE before running 
May need Visual Studio for Mac or Windows


-----FINDINGS-----
-There were no specific requirements apart from focusing on the following resources.
GET /posts/1
GET /posts/1/comments

Normally, we would expect the requirements in more details i.e. functional and non-functional requirements 

-This thus deems the API very difficult to sign off through the production quality gate as the requirement on what
to test is ambiguous.

-If in a professional environment, I will seek to get more details on the acceptance criteria from BA, business, scrum master or test manager by
arranging a code review/test plan meeting and get a sign off for the tests I have written.

-This will show a collaborated effort of obtaining requirements and will start a conversation to get a set
of requirements for the API to shape the testing to the right direction.

-The responses for posts and comments where fairly quick (I have set a timer and no test took longer than 5 seconds)
but this may need a load and stress test to make sure we can accept the API in terms of performance.

-There were no API security to call any of the resources thus deeming the API susceptible to security flaws.

-It allows http calls and this can be considered as a security flaw.

-DELETE a post or comment returns a 200 but no confirmation in the body

-POST a comment or post returns the id of the newly created post

Further findings:
-Response is a wellformed JSON object.
-All the negative testing passed as the API handled it gracefully.
-Response structure is according to data model on the JSON placeholder website ie. field names types are as expected although I had to convert all to a string to process
-Values were returned as expected from the JSON Placeholder website.
-There were no State changes for GET requests.
-Delete of posts and comments with an incorrect id (does not exist i.e. 10000) returned with an OK response
-No Specific requirements on the headers of the API but it looks well formed
-Date returned from Header was an hour behind from my system date time
-Content type returned correctly 'application/json; charset=utf-8'
-The returned cookie is not secured
-No information is leaked with Header X-Powered-By


-----FUTURE IMPROVEMENTS-----
	Add SpecFlow on top of the RestSharp to get non-technical people involved in adding tests.
	Approve the scope of API to cover other resources i.e. albums, photos, todos, users. This was not in scope of testing
