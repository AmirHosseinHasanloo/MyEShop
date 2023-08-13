using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;

namespace MyEShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(db.UsersRepository.GetAll().ToList());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.UsersRepository.GetById(id.Value);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.RolesRepository.GetAll(), "RoleID", "RoleTitle");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,RoleID,UserName,Email,Password,ActiveCode,IsActive,RegisterDate")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.RegisterDate = DateTime.Now;
                users.ActiveCode = Guid.NewGuid().ToString();
                users.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(users.Password, "MD5");
                db.UsersRepository.Insert(users);
                db.UsersRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.RolesRepository.GetAll(), "RoleID", "RoleTitle", users.RoleID);
            return View(users);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.UsersRepository.GetById(id.Value);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.RolesRepository.GetAll(), "RoleID", "RoleTitle", users.RoleID);
            return View(users);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,RoleID,UserName,Email,Password,ActiveCode,IsActive,RegisterDate")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(users.Password, "MD5");
                db.UsersRepository.Update(users);
                db.UsersRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.RolesRepository.GetAll(), "RoleID", "RileTitle", users.RoleID);
            return View(users);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.UsersRepository.GetById(id.Value);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.UsersRepository.GetById(id);
            db.UsersRepository.Delete(users);
            db.UsersRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Admins()
        {
            return View(db.UsersRepository.GetAll().Where(u => u.RoleID == 2));
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
