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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string firstColor, string secondColor)
        {
            return View();
        }
    }
}