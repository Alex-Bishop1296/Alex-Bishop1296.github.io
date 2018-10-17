using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proj.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult Converter()
        {
            // Create the strings to contain values and notify the debugger that you have done so
            string usrInput = Request.QueryString["miles"];
            string radioField = Request.QueryString["units"];
            System.Diagnostics.Debug.WriteLine("Creating strings fields for input and radios");

            // Convert the user miles to a double
            double result = Convert.ToDouble(usrInput);
            System.Diagnostics.Debug.WriteLine("Converting user input to result double");


            // Switch statement based on the result of the radio field
            switch (radioField)
            {
                case "millimeters":
                    result = result * 1609344;
                    break;
                case "centimeters":
                    result = result * 160934.4;
                    break;
                case "meters":
                    result = result * 1609.344;
                    break;
                case "kilometers":
                    result = result * 1.609344;
                    break;
                default:
                    result = -1;
                    break;
            }
            System.Diagnostics.Debug.WriteLine("Converting answer based on radio field");

            //Holds title message for the h2 element
            ViewBag.Message = "Convert Miles to Metric";
            return View();

        }
    }
}