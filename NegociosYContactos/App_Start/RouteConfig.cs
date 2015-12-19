﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NegociosYContactos
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Room",
             url: "{controller}/{action}/{searchWord}",
             defaults: new { controller = "Search", action = "Room" }
         );
            routes.MapRoute(
               name: "Search",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Search", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "Admin",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
         );
        }
    }
}
