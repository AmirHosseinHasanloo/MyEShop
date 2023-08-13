using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEShop.Controllers
{
    public class HomeController : Controller
    {
      private UnitOfWork _db = new UnitOfWork();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutUS()
        {
            return View();
        }

        public ActionResult Slider()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            return PartialView(_db.SliderRepository.GetAll().Where(s => s.IsActive == true && s.StartDate <= dt && s.EndDate >= dt));
        }

        public ActionResult Visitsite()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0); 
            DateTime Yesterday = dt.AddDays(-1);
            DateTime LastMonth = dt.AddDays(-30);
            DateTime LastYear = dt.AddDays(-365);


            SiteVisitViewModel Visit = new SiteVisitViewModel();
            Visit.VisitSum = _db.SiteVisitRepository.GetAll().Count();
            Visit.VisitToDay = _db.SiteVisitRepository.GetAll().Count(v => v.VisitDate == dt);
            Visit.VisitYesterday = _db.SiteVisitRepository.GetAll().Count(v => v.VisitDate == Yesterday);
            Visit.VisitInLastMonth = _db.SiteVisitRepository.GetAll().Count(v => v.VisitDate == LastMonth);
            Visit.VisitInLastYear = _db.SiteVisitRepository.GetAll().Count(v => v.VisitDate == LastYear);
            Visit.Online  = int.Parse(HttpContext.Application["Online"].ToString());

            return PartialView(Visit);
        }

    }
}