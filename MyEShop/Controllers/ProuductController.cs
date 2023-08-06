using DataLayer;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace MyEShop.Controllers
{
    public class ProuductController : Controller
    {
        MyEshopContext db = new MyEshopContext();
        public ActionResult ShowGroups()
        {
            return PartialView(db.Product_Groups.ToList());
        }
        public ActionResult LastProducts()
        {
            return PartialView(db.Products.OrderByDescending(P => P.CreateDate).Take(12).ToList());
        }

        [Route("ShowProduct/{id}")]
        public ActionResult ShowProduct(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var ProductPage = db.Products.Find(id);
            ViewBag.ProductFeatures = ProductPage.Product_Features.DistinctBy(F => F.FeatureID).Select(F => new ShowProductFeatureViewModel()
            {
                FeatureTitle = F.Features.FeatureTitle,
                Values = db.Product_Features.Where(PF => PF.FeatureID == F.FeatureID && PF.ProductID == id).Select(PF => PF.Value).ToList()
            }).ToList();
            return View(ProductPage);
        }

        public ActionResult ShowComments(int id)
        {
            return PartialView(db.Product_Comments.Where(P => P.ProductID == id).ToList());
        }

        public ActionResult CreateComment(int id)
        {
            return PartialView(new Product_Comments()
            {
                ProductID = id
            });
        }

        [HttpPost]
        public ActionResult CreateComment(Product_Comments product_Comments)
        {
            if (ModelState.IsValid)
            {
                product_Comments.CreateDate = DateTime.Now;
                db.Product_Comments.Add(product_Comments);
                db.SaveChanges();

                return PartialView("ShowComments", db.Product_Comments.Where(P => P.ProductID == product_Comments.ProductID).ToList());
            }
            return PartialView(product_Comments);
        }

        [Route("Archive")]
        public ActionResult ProductArchive(int pageId = 1, string title = "", int minPrice = 0, int maxPrice = 0, List<int> selectedGroup = null)
        {
            ViewBag.groups = db.Product_Groups.ToList();

            List<Products> List = new List<Products>();

            ViewBag.SelectedGroups = selectedGroup;

            if (selectedGroup != null && selectedGroup.Any())
            {
                foreach (var group in selectedGroup)
                {
                    List.AddRange(db.Product_Selected_Groups.Where(g => g.GroupID == group).Select(g => g.Products).ToList());
                }
                List = List.Distinct().ToList();
            }
            else
            {
                List.AddRange(db.Products.ToList());
            }

            if (title != null)
            {
                List = List.Where(P => P.Title.Contains(title)).ToList();
                ViewBag.ProductTitle = title;
            }
            if (minPrice != 0)
            {
                List = List.Where(P => P.Price >= minPrice).ToList();
                ViewBag.ProductMinPrice = minPrice;
            }
            if (maxPrice != 0)
            {
                List = List.Where(P => P.Price <= maxPrice).ToList();
                ViewBag.ProductMaxPrice = maxPrice;
            }

            // Pagging System =>
            ViewBag.pageId = pageId;
            int take = 12;
            int skip = (pageId - 1) * take;
            ViewBag.Count = List.Count() / take;


            return View(List.OrderByDescending(p => p.CreateDate).Skip(skip).Take(take));
        }

    }
}