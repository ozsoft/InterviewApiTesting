
-----INTRODUCTION-----
This is the document to outline the findings from testing JSON Placeholder API


-----FRAMEWORK DEPENDENCIES-----
Focused on the following URLs:
	/posts
	/posts/1
	/posts/1/comments
	/comments?postId=1
	/posts?userId=1

I have used the following to develop the project:
Visual Studio 2017 on Windows 7
Visual Studio for Mac 8.7.3 (cloned from github)
C# Language
Developed in .NET Core 2.1
NUnit application solution/project

Dependencies required to run tests: 
Microsoft.NET.Test.Sdk (15.8.0)
NUnit (3.10.1)
NUnit3TestAdapter (3.10.0)
RestSharp (106.11.4)


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
to test is ambiguous

-I will seek to get details on the acceptance criteria from BA, business, scrum master or test manager by
arranging a code review/test plan meeting and get a sign off for the tests I have written

-This will show a collaborated effort of obtaining requirements and will start a conversation to get a set
of requirements for the API to shape the testing to the right direction

-The responses for posts and comments where fairly quick (I have set a timer and no test took longer than 5 seconds)
but this may need a load and stress test to make sure we can accept the API in terms of performance

-There were no API security to call any of the resources thus deeming the API susceptible to security flaws

-It allows http calls and this can be considered as a security flaw

-Delete or Updating a post or comment does not return a valid response if it is successful or not




-----FUTURE IMPROVEMENTS-----
	Add SpecFlow on top of the RestSharp to get non-technical people involved in adding tests
	Approve the scope of API to cover other resources
	
