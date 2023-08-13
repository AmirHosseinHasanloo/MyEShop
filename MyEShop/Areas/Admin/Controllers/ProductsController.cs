using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Web.Mvc;
using DataLayer;
using InsertShowImage;
using KooyWebApp_MVC.Classes;

namespace MyEShop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private MyEshopContext _db = new MyEshopContext();
        private UnitOfWork db = new UnitOfWork();

        IProductTagsRepository productTagsRepository;
        IProduct_Selected_GroupsRepository selectedGroupsRepository;
        public ProductsController()
        {
            productTagsRepository = new ProductTagsRepository(_db);
            selectedGroupsRepository = new Product_Selected_GroupsRepository(_db);
        }


        #region Index-Create-Edit-Delete

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(db.ProductsRepository.GetAll());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.ProductsRepository.GetById(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.Groups = db.Product_GroupsRepository.GetAll();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Title,ShortDescription,Text,Price,ImageName,CreateDate")] Products products, List<int> selectedGroups, HttpPostedFileBase ImageUP, string tags)
        {
            if (ModelState.IsValid)
            {
                if (selectedGroups == null)
                {
                    ViewBag.Error = true;
                    ViewBag.Groups = db.Product_GroupsRepository.GetAll();
                    return View(products);
                }
                products.ImageName = "No_images_available.png";
                if (ImageUP != null && ImageUP.IsImage())
                {
                    products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUP.FileName);
                    ImageUP.SaveAs(Server.MapPath("/Images/ProductImages/" + products.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + products.ImageName), Server.MapPath("/Images/ProductImages/Thumbnail/" + products.ImageName));
                }
                products.CreateDate = DateTime.Now;
                if (!string.IsNullOrEmpty(tags))
                {
                    string[] Tags = tags.Split(',');
                    foreach (var item in Tags)
                    {
                        db.Product_TagsRepository.Insert(new Product_Tags()
                        {
                            ProductID = products.ProductID,
                            Tag = item.Trim()
                        });
                    }
                }
                db.ProductsRepository.Insert(products);
                if (selectedGroups != null && selectedGroups.Any())
                {
                    foreach (var selectedGroup in selectedGroups)
                    {
                        db.Product_Selected_GroupsRepository.Insert(new Product_Selected_Groups()
                        {
                            ProductID = products.ProductID,
                            GroupID = selectedGroup
                        });
                    }
                }
                db.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Groups = db.Product_GroupsRepository.GetAll();
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.ProductsRepository.GetById(id.Value);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tags = string.Join(",", db.Product_TagsRepository.GetAll().Where(t => t.ProductID == id).Select(t => t.Tag).ToList());
            ViewBag.Selected_Groups = products.Prodct_Selected_Groups.ToList();
            ViewBag.Groups = db.Product_GroupsRepository.GetAll();
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Title,ShortDescription,Text,Price,ImageName,CreateDate")] Products products, List<int> selectedGroups, HttpPostedFileBase ImageUP, string Tags)
        {
            if (ModelState.IsValid)
            {
                if (ImageUP != null)
                {
                    if (ImageUP.FileName != null && ImageUP.FileName != "No_images_available.png")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + products.ImageName));
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumbnail/" + products.ImageName));
                    }
                    if (ImageUP.IsImage())
                    {
                        products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUP.FileName);
                        ImageUP.SaveAs(Server.MapPath("/Images/ProductImages/" + products.ImageName));
                        ImageResizer img = new ImageResizer();
                        img.Resize(Server.MapPath("/Images/ProductImages/" + products.ImageName), Server.MapPath("/Images/ProductImages/Thumbnail/" + products.ImageName));
                    }

                }

                ViewBag.Groups = db.Product_GroupsRepository.GetAll();
                products.CreateDate = DateTime.Now;

                productTagsRepository.UpdateTags(products);
                productTagsRepository.Save();
                if (!string.IsNullOrEmpty(Tags))
                {
                    string[] NewTags = Tags.Split(',');
                    foreach (var WritedTags in NewTags)
                    {
                        db.Product_TagsRepository.Insert(new Product_Tags()
                        {
                            ProductID = products.ProductID,
                            Tag = WritedTags.Trim(),
                        });
                    }
                }

                selectedGroupsRepository.UpdateSelectedGroups(products);
                selectedGroupsRepository.Save();
                foreach (var NewSelectedGroups in selectedGroups)
                {
                    db.Product_Selected_GroupsRepository.Insert(new Product_Selected_Groups()
                    {
                        ProductID = products.ProductID,
                        GroupID = NewSelectedGroups,
                    });
                }

                db.ProductsRepository.Update(products);
                db.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Tags = Tags;
            ViewBag.Selected_Groups = selectedGroups;
            ViewBag.Groups = db.Product_GroupsRepository.GetAll();
            return View(products);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.ProductsRepository.GetById(id.Value);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.ProductsRepository.GetById(id);
            db.ProductsRepository.Delete(products);
            db.Save();
            return RedirectToAction("Index");
        }
        #endregion

        #region Gallery
        public ActionResult ProductGallery(int id)
        {
            ViewBag.Galleries = db.Product_GalleriesRepository.GetAll().Where(g => g.ProductID == id).ToList();
            return View(new Product_Galleries()
            {
                ProductID = id
            });
        }

        [HttpPost]
        public ActionResult ProductGallery(Product_Galleries gallery, HttpPostedFileBase ImageUP)
        {
            if (ModelState.IsValid)
            {
                if (ImageUP != null && ImageUP.IsImage())
                {
                    gallery.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUP.FileName);
                    ImageUP.SaveAs(Server.MapPath("/Images/ProductImages/" + gallery.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + gallery.ImageName), Server.MapPath("/Images/ProductImages/Thumbnail/" + gallery.ImageName));
                }
                db.Product_GalleriesRepository.Insert(gallery);
                db.Save();
            }

            return RedirectToAction("ProductGallery", new { id = gallery.ProductID });
        }

        public ActionResult DeleteGallery(int id)
        {
            var picure = db.Product_GalleriesRepository.GetById(id);

            System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + picure.ImageName));
            System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumbnail/" + picure.ImageName));

            db.Product_GalleriesRepository.Delete(picure);
            db.Save();
            return RedirectToAction("ProductGallery", new { id = picure.ProductID });
        }

        #endregion


        #region Features
        public ActionResult ProductFeatures(int id)
        {
            ViewBag.Features = db.Product_FeaturesRepository.GetAll().Where(F => F.ProductID == id).ToList();
            ViewBag.FeatureID = new SelectList(_db.Features, "FeatureID", "FeatureTitle");
            return View(new Product_Features()
            {
                ProductID = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductFeatures(Product_Features model)
        {
            db.Product_FeaturesRepository.Insert(model);
            db.Save();
            return RedirectToAction("ProductFeatures", new { id = model.ProductID });
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
