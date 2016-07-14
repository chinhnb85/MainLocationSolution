﻿using LibCore.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibCore.Security.Crypt;
using ModelCMS.User;
using ModelCMS.Base;
using MainLocation.Code;

namespace MainLocation.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]        
        public JsonResult Login(string userName, string password)
        {
            try
            {
                var ipl = SingletonIpl.GetInstance<IplUser>();
                var ue = new UserEntity();
                var res = ipl.Login(userName, password.Trim(), ref ue);//Md5Util.Md5EnCrypt(password.Trim())

                if (res)
                {
                    var userSess = new UserSession
                    {
                        UserName = ue.UserName,
                        UserType = ue.UserType,
                        Id = int.Parse(ue.Id.ToString()),
                        DisplayName = ue.DisplayName
                    };
                    SessionUtility.SetUser(userSess);
                    Session["UserName"] = userName;
                }

                return Json(new { status = res }, JsonRequestBehavior.AllowGet);
            }
            catch
            {                
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Logout()
        {
            SessionUtility.ClearSession();
            return RedirectToAction("Index", "Home");
        }
    }
}