using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyEShop.Areas.UserPanel.Controllers
{
    public class UserAccountController : Controller
    {
        MyEshopContext db = new MyEshopContext();
        // GET: Admin/Account
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string OldPasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(model.OldPassword, "MD5");
                var user = db.Users.SingleOrDefault(U => U.UserName == User.Identity.Name && U.Password == OldPasswordHash);

                if (user != null)
                {
                    string HashNewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "MD5");

                    user.Password = HashNewPassword;
                    db.SaveChanges();
                    ViewBag.Success = true;
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "رمز عبور فعلی شما درست نمی باشد");
                }
            }
            return View();
        }
    }
}