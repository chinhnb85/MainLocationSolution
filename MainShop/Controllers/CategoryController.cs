using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainShop.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category/Created        
        public ActionResult Created()
        {
            return View();
        }

        // GET: Category/List
        public ActionResult List()
        {
            return View();
        }
    }
}