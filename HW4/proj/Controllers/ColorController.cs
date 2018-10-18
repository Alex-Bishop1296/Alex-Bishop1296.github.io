using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proj.Controllers
{
    public class ColorController : Controller
    {
        // GET: Color
        public ActionResult Create()
        {
            return View();
        }
    }
}