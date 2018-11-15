using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InternetLT
{
    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //As per the requirements for this assignment we made a new route in route config
            routes.MapRoute(
               name: "RetrieveGiphy",
               // url is name of controller, method used, and then name of string taken in
               url: "{controller}/{action}/{word}",
               defaults: new { controller = "Word", action = "RetrieveGiphy" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
