using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using Asp_Mvc_2015_2016.Models;
using Asp_Mvc_2015_2016.Migrations;
namespace Asp_Mvc_2015_2016
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
         //   UnityConfig.RegisterComponents(); //VinzzB: TODO: register ioc (unity)
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
