using DataLayer;
using MyEshop;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyEShop.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWork _db = new UnitOfWork();

        [Route("Register")]
        public ActionResult Retgister()
        {
            ViewBag.TitleName = "ثبت نام";
            return View();
        }
        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Retgister(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (!_db.UsersRepository.GetAll().Any(U => U.Email == register.Email.Trim().ToLower()))
                {
                    Users user = new Users()
                    {
                        Email = register.Email.Trim().ToLower(),
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                        ActiveCode = Guid.NewGuid().ToString(),
                        IsActive = false,
                        RegisterDate = DateTime.Now,
                        RoleID = 1,
                        UserName = register.UserName
                    };
                    _db.UsersRepository.Insert(user);
                    _db.Save();


                    //Send Active Email
                    string body = PartialToStringClass.RenderPartialView("ManageEmails", "ActivationEmail", user);
                    SendEmail.Send(user.Email, "ایمیل فعال سازی", body);
                    //End Send Active Email

                    return View("SuccessedRegister", user);
                }

                else
                {
                    ModelState.AddModelError("Email", "ایمیل شما تکراری است");
                }
            }
            return View(register);
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginViewModel login, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                string HashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var Emailvalidate = _db.UsersRepository.GetAll().SingleOrDefault(U => U.Email == login.Email.Trim().ToLower()&&U.Password==HashPassword);

                if (Emailvalidate != null)
                {
                    if (Emailvalidate.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(Emailvalidate.UserName, login.RememberMe);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "کاربر گرامی حساب شما فعال نمی باشد");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربر گرامی کاربری با این اطلاعات ثبت نام نکرده است ");
                }
            }
            return View(login);
        }

        public ActionResult ActiveUser(string id)
        {
            var IsExistsUser = _db.UsersRepository.GetAll().SingleOrDefault(U => U.ActiveCode == id);

            if (IsExistsUser == null)
            {
                return HttpNotFound();
            }
            IsExistsUser.IsActive = true;
            IsExistsUser.ActiveCode = Guid.NewGuid().ToString();
            ViewBag.UserName = IsExistsUser.UserName;
            _db.Save();
            return View();
        }

        [Route("SignOut")]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Route("ForgotPassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.UsersRepository.GetAll().SingleOrDefault(U => U.Email.Trim().ToLower() == model.Email.Trim().ToLower());
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        // Send Recovery Password Email
                        string body = PartialToStringClass.RenderPartialView("ManageEmails", "ResetPassword", user);
                        SendEmail.Send(model.Email, "ایمیل بازیابی رمز عبور", body);
                        // End Send Recovery Password Email

                        return View("SuccessedRecoveryPass",user);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "کاربر عزیز این حساب کاربری فعال نمی باشد!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربر گرامی کاربری با این ایمیل ثبت نام نشده است ");
                }

            }
            return View(model);
        }

        public ActionResult RecoveryPass(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecoveryPass(string id,RecoveryPasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = _db.UsersRepository.GetAll().SingleOrDefault(U => U.ActiveCode == id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var HashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5");
                user.ActiveCode = Guid.NewGuid().ToString();
                _db.Save();
                return Redirect("/Login?recovery=true");
            }

            return View(model);
        }

    }
}