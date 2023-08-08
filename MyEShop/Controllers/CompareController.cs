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
            List<DataLayer.CompareItem> ComList = new List<DataLayer.CompareItem>();

            if (Session["Compare"] != null)
            {
                ComList = Session["Compare"] as List<DataLayer.CompareItem>;
            }
            List<DataLayer.Features> features = new List<DataLayer.Features>();

            // Product Features 
            List<Product_Features> productsFeature = new List<Product_Features>();

            foreach (var item in ComList)
            {
               features.AddRange(db.Product_Features.Where(p=>p.ProductID==item.ProductID).Select(p=>p.Features).ToList());
                productsFeature.AddRange(db.Product_Features.Where(p=>p.ProductID==item.ProductID).ToList());
            }
            ViewBag.Features = features.Distinct().ToList();
            ViewBag.ProductFeatures = productsFeature;
            return View(ComList);
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
            return PartialView("ListCompare", ComList);
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

        public ActionResult DeleteFromCompare(int id)
        {
            List<DataLayer.CompareItem> ComList = new List<DataLayer.CompareItem>();

            if (Session["Compare"] != null)
            {
                ComList = Session["Compare"] as List<DataLayer.CompareItem>;
                int delete = ComList.FindIndex(c => c.ProductID == id);
                ComList.RemoveAt(delete);
                Session["Compare"] = ComList;
            }
            return PartialView("ListCompare", ComList);
        }

    }
}