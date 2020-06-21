using SearchTestApp.Controllers;
using SearchTestApp.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace SearchTestApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}