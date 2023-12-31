﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using KooyWebApp_MVC.Classes;

namespace MyEShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SlidersController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: Admin/Sliders
        public ActionResult Index()
        {
            return View(db.SliderRepository.GetAll());
        }

        // GET: Admin/Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.SliderRepository.GetById(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Admin/Sliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlideID,Title,ImageName,StartDate,EndDate,IsActive,Url,ImageUP")] Slider slider, HttpPostedFileBase ImageUP)
        {
            if (ModelState.IsValid)
            {
                if (ImageUP == null)
                {
                    ModelState.AddModelError("ImageName", "لطفا تصویر را انتخاب کنید");
                    return View(slider);
                }
                slider.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUP.FileName);
                ImageUP.SaveAs(Server.MapPath("/Images/SliderImages/" + slider.ImageName));
                db.SliderRepository.Insert(slider);
                db.SliderRepository.Save();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Admin/Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.SliderRepository.GetById(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlideID,Title,ImageName,StartDate,EndDate,IsActive,Url,ImageUP")] Slider slider, HttpPostedFileBase ImageUP)
        {
            if (ModelState.IsValid)
            {
                if (ImageUP != null)
                {
                    System.IO.File.Delete(Server.MapPath("/Images/SliderImages/" + slider.ImageName));
                    slider.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUP.FileName);
                    ImageUP.SaveAs(Server.MapPath("/Images/SliderImages/" + slider.ImageName));
                }
                db.SliderRepository.Update(slider);
                db.SliderRepository.Save();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Admin/Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.SliderRepository.GetById(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.SliderRepository.GetById(id);
            System.IO.File.Delete(Server.MapPath("/Images/SliderImages/" + slider.ImageName));
            db.SliderRepository.Delete(slider);
            db.SliderRepository.Save();
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
