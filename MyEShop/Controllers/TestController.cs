using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace MyEShop.Controllers
{
    public class TestController : Controller
    {
        [Authorize]
        public ActionResult Test1()
        {
            return View();
        }
        [Authorize (Roles ="admin")]
        public ActionResult Test2()
        {
            return View();
        }

    }
}