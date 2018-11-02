using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigData.Models;
using BigData.Models.ViewModels;

namespace BigData.Controllers
{
    public class QueryController : Controller
    {
        // load the model into our controller

        private UserContext db = new UserContext();

        [HttpGet]
        public ActionResult Search(string input)
        {

            // If the search string is empty return basic view
            if (input == "" || input == null)
            {
                return View();
            }

            // Run the search
            else
            {
                List<PersonVM> Person = db.People
                                        .Where(n => n.FullName.Contains(input))
                                        .Select(n => new PersonVM
                                        {
                                            FullName = n.FullName
                                        }).ToList();
                ViewBag.Result = true;
                return View(Person);
            }
        }

        public ActionResult Details(string Name)
        {
            if (Name == null || Name == "")
            {
                // Redirect to search page if URL is edited
                return RedirectToAction("Search");
            }

            // Otherwise run the search
            else
            {
                List<PersonVM> IndividualDetails = db.People
                                                   .Where(x => x.FullName.Equals(Name))
                                                   .Select(x => new PersonVM
                                                   {
                                                       FullName = x.FullName,
                                                       PreferredName = x.PreferredName,
                                                       PhoneNumber = x.PhoneNumber,
                                                       FaxNumber = x.FaxNumber,
                                                       EmailAddress = x.EmailAddress,
                                                       ValidFrom = x.ValidFrom,
                                                   }).ToList();
                return View(IndividualDetails);

            }
        }
    }
}