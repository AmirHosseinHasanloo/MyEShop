using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEShop.Areas.Admin.Controllers
{
    public class SearchController : Controller
    {
        MyEshopContext db = new MyEshopContext();
        // GET: Admin/Search
        public ActionResult Index(string q)
        {
            ViewBag.Search = q;
            List<Products> List = new List<Products>();
            List.AddRange(db.Product_Tags.Where(T => T.Tag == q).Select(T => T.Products).ToList());
            List.AddRange(db.Products.Where(P=>P.Title.Contains(q)||P.ShortDescription.Contains(q)||P.Text.Contains(q)).ToList());
            return View(List.Distinct());
        }
    }
}