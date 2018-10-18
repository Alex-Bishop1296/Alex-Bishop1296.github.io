using System;
using System.Collections.Generic;
using System.Drawing;
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

        /// <summary>
        /// POST method for displaying the asnwer of a color addition
        /// </summary>
        /// <param name="firstColor"> The hexstring for the first value the user entered</param>
        /// <param name="secondColor"> The hexstring for the second value the user entered</param>
        /// <returns> The view with the mixed color for the user</returns>
        [HttpPost]
        public ActionResult Create(string firstColor, string secondColor)
        {
            // Print the colors given to the debug to confirm they were passed correctly
            System.Diagnostics.Debug.WriteLine("firstColor = " + firstColor);
            System.Diagnostics.Debug.WriteLine("secondColor = " + secondColor);

            // Check for null trigger
            if (firstColor != null && secondColor != null)
            {
                // Put both items in color objects
                Color colorOne = ColorTranslator.FromHtml(firstColor);
                Color colorTwo = ColorTranslator.FromHtml(secondColor);

                // Print the colors translated to confirm they are expected values
                System.Diagnostics.Debug.WriteLine("colorOne = " + Convert.ToString(colorOne));
                System.Diagnostics.Debug.WriteLine("colorTwo = " + Convert.ToString(colorTwo));

                // Mix the two colors via ints
                // Mix Alpha with overflow check
                int mixA = colorOne.A + colorTwo.A;
                if (mixA > 1) mixA = 1;
                // Mix Red with overflow check
                int mixR = colorOne.R + colorTwo.R;
                if (mixR > 255) mixR = 255;
                // Mix Green with overflow check
                int mixG = colorOne.G + colorTwo.G;
                if (mixG > 255) mixG = 255;
                // Mix Blue with overflow check
                int mixB = colorOne.B + colorTwo.B;
                if (mixB > 255) mixB = 255;

                // Mix the colors and print Debug message to check mix
                Color colorMix = Color.FromArgb(mixA, mixR, mixG, mixB);
                System.Diagnostics.Debug.WriteLine("mixColor = " + Convert.ToString(colorMix));

                // Assign values for viewbag for use in html
                ViewBag.firstC = "width: 100px; height: 100px; border: 2px solid #000000; background: " + ColorTranslator.ToHtml(colorOne) + "; ";
                ViewBag.secondC = "width: 100px; height: 100px; border: 2px solid #000000; background: " + ColorTranslator.ToHtml(colorTwo) + "; ";
                ViewBag.mixC = "width: 100px; height: 100px; border: 2px solid #000000; background: " + ColorTranslator.ToHtml(colorMix) + "; ";
                ViewBag.plus = "+";
                ViewBag.equ = "=";

            }

            return View();
        }
    }
}