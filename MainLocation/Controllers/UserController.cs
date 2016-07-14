using MainLocation.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainLocation.Controllers
{
    public class UserController : BaseController
    {
        // GET: User/Created
        public ActionResult Created()
        {
            return View();
        }
    }
}