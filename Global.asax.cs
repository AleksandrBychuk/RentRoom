using Microsoft.EntityFrameworkCore;
using RentRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RentRoom
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RentRoomContext context = new RentRoomContext();
            SampleData.Initialize(context);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
