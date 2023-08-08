using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using GSD.Globalization;
using System.Threading;
using DataLayer;
using System.Web.WebSockets;

namespace MyEShop
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var persianCulture = new PersianCulture();
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;
        }
        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }

        protected void Session_Start()
        {
            string ip = Request.UserHostAddress;
            DateTime dtNow = new DateTime(DateTime.Now.Year,DateTime.Now.Month, DateTime.Now.Day,0,0,0);
            using (MyEshopContext db = new MyEshopContext())
            {
                if (!db.SiteVisit.Any(v => v.IP == ip && v.VisitDate == dtNow))
                {                    
                    db.SiteVisit.Add(new SiteVisit()
                    {
                        VisitDate = DateTime.Now.Date,
                        IP = ip
                    });

                    db.SaveChanges();
                }
            }
        }
    }
}