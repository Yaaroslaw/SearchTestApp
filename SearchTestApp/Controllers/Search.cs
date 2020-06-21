using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SearchTestApp.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace SearchTestApp.Controllers
{

    public class UserSearch 
    {
        private ISearch _search = null;

        public UserSearch(ISearch search)
        {
            _search = search;
        }
        public int MakeSearch(string query)
        {           
            var result = _search.Search(query);
            return result;
        }


    }
    public class  GoogleSearch: Controller,  ISearch
    {
        public int Search(string query)
        {
            string searchQuery = query;
            string cx = "006461571065944352717:p9f6c6saqk8";
            string apiKey = "AIzaSyApsQ4tH6JsP0TxoJkPpUBBgHOs-aThJOs";

            var request = WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=" + apiKey + "&cx=" + cx + "&q=" + searchQuery +"& alt = json & fields = queries(request(totalResults))");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseString = reader.ReadToEnd();

            dynamic jsonData = JsonConvert.DeserializeObject(responseString);
            var result = jsonData.searchInformation["totalResults"];
            return Int32.Parse(result.ToString());
        }
       
    }
    public class BingSearch : Controller, ISearch
    {
        const string accessKey = "61c4794fb19c49129b01e3a87d43f0a8";  //VALID FOR SEVEN DAYS
        const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";


        public int Search(string query)
        {
            var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(query);
            WebRequest request = HttpWebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = accessKey;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;

            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var jsonDic = JObject.Parse(json);

            var webPages = jsonDic["webPages"];
            var totalEstimatedMatches = JObject.Parse(webPages.ToString())["totalEstimatedMatches"].ToString();
            return Int32.Parse(totalEstimatedMatches);
        }

    }


}