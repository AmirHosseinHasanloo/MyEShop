using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using MyEShop.Controllers;

namespace MyEShop.Areas.Admin.Controllers
{
    public class ProductGroupsController : Controller
    {
        private MyEshopContext db = new MyEshopContext();

        // GET: Admin/ProductGroups
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListGroups()
        {
            var product_Groups = db.Product_Groups;
            return PartialView(product_Groups.ToList());
        }

        // GET: Admin/ProductGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Groups product_Groups = db.Product_Groups.Find(id);
            if (product_Groups == null)
            {
                return HttpNotFound();
            }
            return View(product_Groups);
        }

        // GET: Admin/ProductGroups/Create
        public ActionResult Create(int? id)
        {
            return PartialView(new Product_Groups()
            {
                ParentID = id,
            });
        }

        // POST: Admin/ProductGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle,ParentID")] Product_Groups product_Groups)
        {
            if (ModelState.IsValid)
            {
                db.Product_Groups.Add(product_Groups);
                db.SaveChanges();
                return PartialView("ListGroups", db.Product_Groups.Where(p => p.ParentID== null));
            }

            ViewBag.ParentID = new SelectList(db.Product_Groups, "GroupID", "GroupTitle", product_Groups.ParentID);
            return View(product_Groups);
        }

        // GET: Admin/ProductGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Groups product_Groups = db.Product_Groups.Find(id);
            if (product_Groups == null)
            {
                return HttpNotFound();
            }
            return PartialView(product_Groups);
        }

        // POST: Admin/ProductGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupTitle,ParentID")] Product_Groups product_Groups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Groups).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("ListGroups", db.Product_Groups.Where(p => p.ParentID == null));
            }
            ViewBag.ParentID = new SelectList(db.Product_Groups, "GroupID", "GroupTitle", product_Groups.ParentID);
            return View(product_Groups);
        }

        // GET: Admin/ProductGroups/Delete/5
        public void Delete(int? id)
        {
            var group = db.Product_Groups.ToList();
            if (group.Any(G => G.ParentID == G.GroupID))
            {
                foreach (var SubGroups in db.Product_Groups.Where(G => G.GroupID == G.ParentID))
                {
                    db.Product_Groups.Remove(SubGroups);
                }
            }
            var GroupItem = db.Product_Groups.Find(id);
            db.Product_Groups.Remove(GroupItem);
            db.SaveChanges();
        }

        // POST: Admin/ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Groups product_Groups = db.Product_Groups.Find(id);
            db.Product_Groups.Remove(product_Groups);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
