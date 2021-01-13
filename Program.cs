using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ApiTest
{
  class Program
  {
    static void Main()
    {
      var apiCallTask = ApiHelper.ApiCall("MCG1weiYSgBg4lezXXHACPFm0NVoJZEf");
      var result = apiCallTask.Result;

      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      List<Article> articleList = JsonConvert.DeserializeObject<List<Article>>(jsonResponse["results"].ToString());

      foreach (Article article in articleList)
      {
        Console.WriteLine($"Section: {article.Section}");
        Console.WriteLine($"Title: {article.Title}");
        Console.WriteLine($"Abstract: {article.Abstract}");
        Console.WriteLine($"Url: {article.Url}");
        Console.WriteLine($"Byline: {article.Byline}");
      }
    }
  }

  class ApiHelper
  {
    public static async Task<string> ApiCall(string apiKey)
    {
      RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2");
      RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.GET);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }
  }
}

// Notice that each key (of the JSON key-value pairs) in the JSON response is in lower snake-case, not the PascalCase commonly used in C#. When retrieving data from a JSON response, always make sure to use the name of the JSON key with the exact casing it appears in the response object

// We use the DeserializeObject() method to create a list of Articles. The method will automatically grab any JSON keys in our response that match the names of the properties in our class. In order for this to work, the property name has to match the JSON key. This means that the Section property for our message class needs to be named Section. We could not rename it to something like Category because the information is named "section" in the JSON data

// lots of new info! lesson 16 link below

// https://www.learnhowtoprogram.com/c-and-net/authentication-with-identity/deserializing-responses