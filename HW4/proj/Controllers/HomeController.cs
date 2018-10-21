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

        /// <summary>
        ///  This Method is used to get the user input (if valid) and display the results based on that input
        /// </summary>
        /// <returns>The view of the page with the requested unit conversion displayed</returns>
        [HttpGet]
        public ActionResult Converter()
        {
            // Create the strings to contain values and notify the debugger that you have done so
            // NOTE: This line here fills the requirement of Request object
            string usrInput = Request.QueryString["miles"];
            string radioField = Request.QueryString["units"];
            double result = -1;
            //used to stop Querystring errors
            bool errorTrip = false;

            // Debug message
            System.Diagnostics.Debug.WriteLine("Creating strings fields for input and radios");

            // Check if the user inputed a number, stop process if they did not
            var isNumber = int.TryParse(usrInput, out int n);
            if (isNumber == false)
            {
                errorTrip = true;
            }


            // Error checking
            if (usrInput != null && usrInput != "" && errorTrip == false)
            {
                // Convert the user miles to a double
                result = Convert.ToDouble(usrInput);
                System.Diagnostics.Debug.WriteLine("Converting user input to result double");

                // Switch statement based on the result of the radio field
                // Actual conversions and calculations happen here
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
                        errorTrip = true;
                        break;
                }
                // Debug message
                System.Diagnostics.Debug.WriteLine("Converting answer based on radio field");

                // The output message for the user
                // Checks if error case was hit
                string statement;
                if (errorTrip == false)
                {
                    statement = usrInput + " miles is equal to " + Convert.ToString(result) + " " + radioField;
                }
                else {
                    statement = "An error has occured via direct user action, please only use the webpage for input";
                }

                ViewBag.statement = statement;
                
            }
           

            //Holds title message for the h2 element, this is static so view does not matter
            ViewBag.Message = "Convert Miles to Metric";
            return View();

        }
    }
}