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
        private MyEshopContext dbContext = new MyEshopContext();
        private UnitOfWork _db = new UnitOfWork();

        // Repositories
        IProductRepository productRepository;
        IProduct_FeaturesRepository featuresRepository;

        public ProuductController()
        {
            productRepository = new ProductRepository(dbContext);
            featuresRepository = new Product_FeaturesRepository(dbContext);
        }


        public ActionResult ShowGroups()
        {
            return PartialView(_db.Product_GroupsRepository.GetAll().ToList());
        }
        public ActionResult LastProducts()
        {
            return PartialView(productRepository.LastProducts());
        }

        [Route("ShowProduct/{id}")]
        public ActionResult ShowProduct(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var ProductPage = _db.ProductsRepository.GetById(id.Value);
            ViewBag.ProductFeatures = featuresRepository.Get_Features(id.Value);
            return View(ProductPage);
        }

        public ActionResult ShowComments(int id)
        {
            return PartialView(_db.Product_CommentsRepository.GetAll().Where(P => P.ProductID == id).ToList());
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
                _db.Product_CommentsRepository.Insert(product_Comments);
                _db.Save();

                return PartialView("ShowComments", _db.Product_CommentsRepository.GetAll().Where(P => P.ProductID == product_Comments.ProductID).ToList());
            }
            return PartialView(product_Comments);
        }

        [Route("Archive")]
        public ActionResult ProductArchive(int pageId = 1, string title = "", int minPrice = 0, int maxPrice = 0, List<int> selectedGroup = null)
        {
            ViewBag.groups = _db.Product_GroupsRepository.GetAll();

            List<Products> List = new List<Products>();

            ViewBag.SelectedGroups = selectedGroup;

            if (selectedGroup != null && selectedGroup.Any())
            {
                foreach (var group in selectedGroup)
                {
                    List.AddRange(_db.Product_Selected_GroupsRepository.GetAll().Where(g => g.GroupID == group).Select(g => g.Products).ToList());
                }
                List = List.Distinct().ToList();
            }
            else
            {
                List.AddRange(_db.ProductsRepository.GetAll());
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