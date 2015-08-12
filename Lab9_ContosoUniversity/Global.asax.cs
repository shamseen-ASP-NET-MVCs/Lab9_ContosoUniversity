using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Lab9_ContosoUniversity.DAL;
using System.Data.Entity.Infrastructure.Interception;

namespace Lab9_ContosoUniversity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ////allowing query interception to log and possibly test resilience
            //DbInterception.Add(new SchoolInterceptorTransientErrors());
            //DbInterception.Add(new SchoolInterceptorLogging());
        }
    }
}
