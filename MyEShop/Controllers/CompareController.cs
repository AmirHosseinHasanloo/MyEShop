using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEShop.Controllers
{
    public class CompareController : Controller
    {
        // GET: Compare
        MyEshopContext db = new MyEshopContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCompare(int id)
        {
            List<DataLayer.CompareItem> ComList = new List<DataLayer.CompareItem>();

            if (Session["Compare"] != null)
            {
                ComList = Session["Compare"] as List<DataLayer.CompareItem>;
            }
            if (!ComList.Any(p => p.ProductID == id))
            {
                var Product = db.Products.Where(p => p.ProductID == id).Select(p => new { p.Title, p.ImageName }).Single();
                ComList.Add(new DataLayer.CompareItem()
                {
                    ProductID = id,
                    ImageName = Product.ImageName,
                    Title = Product.Title,
                });
            }
            Session["Compare"] = ComList;
            return PartialView("ListCompare",ComList);
        }

        public ActionResult ListCompare()
        {
            List<DataLayer.CompareItem> ComList = new List<DataLayer.CompareItem>();

            if (Session["Compare"] != null)
            {
                ComList = Session["Compare"] as List<DataLayer.CompareItem>;
            }
            return PartialView(ComList);
        }

    }
}