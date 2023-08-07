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
        MyEshopContext db = new MyEshopContext();
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
            DateTime dt = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0);
            
            return PartialView(db.Slider.Where(s => s.IsActive==true && s.StartDate<=dt && s.EndDate >=dt));
        }
    }
}