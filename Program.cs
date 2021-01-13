using System;
using System.Threading.Tasks;
using RestSharp;

namespace ApiTest
{
  class Program
  {
    static void Main()
    {
      var apiCallTask = ApiHelper.ApiCall("MCG1weiYSgBg4lezXXHACPFm0NVoJZEf"); // apiCallTask is where we store the returned Task from out async ApiCall function and we use the built in ApiHelper.ApiCall to do so
      var result = apiCallTask.Result; //we create a variable to store the Result of the Task, which in our case is a string representation of the API call's response content
      Console.WriteLine(result);
    }
  }

  class ApiHelper
  {
    public static async Task<string> ApiCall(string apiKey) // we want the API calls to be async
    {
      RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2"); // RestClient is a RestSharp object. we store the connection in the client variable
      RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.GET); // this is the API request. using C# string interpolation for the api key - like JS template literals
      var response = await client.ExecuteTaskAsync(request);
      return response.Content; // return the Content property of the response variable which is a string representation of the response content
    }
  }
}