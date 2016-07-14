using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainLocation.Controllers.Base;
using ModelCMS.User;
using LibCore.EF;

namespace MainLocation.Controllers
{
    public class AccountController : BaseController
    {
        // GET: /Account
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        public JsonResult ListAll()
        {
            var total = 0;
            var ipl = SingletonIpl.GetInstance<IplUser>();
            var data = ipl.ListAll();

            return Json(new
            {
                status = total > 0,
                Data = data,
                totalCount = total
            }, JsonRequestBehavior.AllowGet);
        }
    }
}