
using SearchTestApp.Interfaces;
using System.Web.Mvc;
using Unity;

namespace SearchTestApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
         
            return View();
        }

        public ActionResult Search(string firstQuery, string secondQuery)
        {
            var container = new UnityContainer();
            container.RegisterType<ISearch, GoogleSearch>();
            var googleSearch = container.Resolve<UserSearch>();
            var firstGoogleResult = googleSearch.MakeSearch(firstQuery);
            var secondGoogleResult = googleSearch.MakeSearch(secondQuery);

            container.RegisterType<ISearch, BingSearch>();
            var bingSearch = container.Resolve<UserSearch>();
            var firstBingResult = bingSearch.MakeSearch(firstQuery);
            var secondBingResult = bingSearch.MakeSearch(secondQuery);

            var result = $"Total Search Results(Google + Bing):{firstQuery}: {firstGoogleResult + firstBingResult} ";
            result += $"{secondQuery}: {secondGoogleResult + secondBingResult} ";
            if(firstGoogleResult + firstBingResult > secondGoogleResult + secondBingResult)
            {
                result += $"Winner: {firstQuery}";
            }
            else
            {
                result += $"Winner: {secondQuery}";
            }
            
            return View((object)result);
        }

    }


}