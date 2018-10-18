using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proj.Controllers
{
    public class Color : Controller
    {
        // GET: Color
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(int? id, int? size, string kind)
        {
            //...]
            return View();
        }
    }
}