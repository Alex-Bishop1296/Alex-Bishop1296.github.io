using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BigData.Models;
using BigData.Models.ViewModels;
using System.Diagnostics;

namespace BigData.Controllers
{
    public class QueryController : Controller
    {
        // load the model into our controller

        private UserContext db = new UserContext();

        /// <summary>
        /// Return the view of the search with results
        /// </summary>
        /// <param name="input">name searched to get results</param>
        /// <returns>either blank search form or search form with names view</returns>
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

        /// <summary>
        /// Return the results a search in detail based on given name
        /// </summary>
        /// <param name="NameEntry">Exact name of the person that was searched for</param>
        /// <returns>view with details of name given</returns>
        [HttpGet]
        public ActionResult Details(string NameEntry)
        {
            //Check if the input is valid
            if (NameEntry == null || NameEntry == "")
            {
                // Redirect to search page if URL is edited
                return RedirectToAction("Search");
            }
            // Get the base details of the listed search of an person in the system
            // This search here is just looking at the people table in the database where the fullname is equal to the one we were given by the parameter
            List<PersonVM> IndividualDetails = db.People
                                                   .Where(x => x.FullName.Equals(NameEntry))
                                                   .Select(x => new PersonVM
                                                   {
                                                       FullName = x.FullName,
                                                       PreferredName = x.PreferredName,
                                                       PhoneNumber = x.PhoneNumber,
                                                       FaxNumber = x.FaxNumber,
                                                       EmailAddress = x.EmailAddress,
                                                       ValidFrom = x.ValidFrom,
                                                   }).ToList();

            //Customer Company Details
            //Close to our last search, but we also use .Include to add the PrimaryContactPersonID table
            var CustomerDetails = db.People
                                    .Where(p => p.FullName == NameEntry)
                                    .Include("PrimaryContactPersonID")
                                    .SelectMany(p => p.Customers2).ToList();

            // If the Customer has no additional details via count, we do not show any feature 2 details
            if (CustomerDetails.Count == 0)
            {
                return View(IndividualDetails);
            }
            else
            {

                //Give the details of items purchased by the customer
                var ItemDetails = db.People
                                    .Where(person => person.FullName.Contains(NameEntry)).Include("PrimaryContactPersonID")
                                    .SelectMany(x => x.Customers2).Include("CustomerID").SelectMany(x => x.Orders)
                                    .Include("OrderID").Include("CustomerID").SelectMany(x => x.Invoices).Include("InvoiceID")
                                    .SelectMany(x => x.InvoiceLines).OrderByDescending(x => x.LineProfit).Take(10).ToList();

                //A list of salesman for the top 10 items sold to the customer.
                var SalesMen = db.People
                                    .Where(person => person.FullName.Contains(NameEntry)).Include("PrimaryContactPersonID")
                                    .SelectMany(x => x.Customers2).Include("CustomerID").SelectMany(x => x.Orders)
                                    .Include("OrderID").Include("CustomerID").SelectMany(x => x.Invoices).Include("InvoiceID")
                                    .SelectMany(x => x.InvoiceLines).OrderByDescending(x => x.LineProfit).Take(10)
                                    .Include("InvoiceID").Select(x => x.Invoice).Include("SalespersonID").Select(x => x.Person4)
                                    .ToList();
                //Items Purchased Details

                List<ItemPurchase> Top10Items = new List<ItemPurchase>();

                //Intializes a list of ItemPurchased classes that contains the details for the top 10 items sold to the customer.
                for (int i = 0; i < 10; i++)
                {
                    Top10Items.Add(new ItemPurchase
                    {
                        StockItemID = ItemDetails.ElementAt(i).StockItemID,
                        ItemDescription = ItemDetails.ElementAt(i).Description,
                        LineProfit = ItemDetails.ElementAt(i).LineProfit,
                        SalesPerson = SalesMen.ElementAt(i).FullName
                    });
                }
                List<PersonVM> Customers = new List<PersonVM>
                {
                    new PersonVM
                    {//Default Details Basic details about the person being searched.
                        FullName = IndividualDetails.First().FullName,
                        PreferredName = IndividualDetails.First().PreferredName,
                        PhoneNumber = IndividualDetails.First().PhoneNumber,
                        FaxNumber = IndividualDetails.First().FaxNumber,
                        EmailAddress = IndividualDetails.First().EmailAddress,
                        ValidFrom = IndividualDetails.First().ValidFrom,

                        //Customer Company Details; See PersonVM.cs. Details about the customer's company.
                        CompanyName = CustomerDetails.First().CustomerName,
                        CompanyPhone = CustomerDetails.First().PhoneNumber,
                        CompanyFax = CustomerDetails.First().FaxNumber,
                        CompanyWebsite = CustomerDetails.First().WebsiteURL,
                        CompanyValidFrom = CustomerDetails.First().ValidFrom,

                        //Purchase History Details; See PersonVM.cs. Total orders, GrossSales and Gross profit for those orders.
                        Orders = db.People.Where(person => person.FullName.Contains(NameEntry)).Include("PrimaryContactPersonID")
                                   .SelectMany(x => x.Customers2).Include("CustomerID").SelectMany(x => x.Orders).Count(),

                        GrossSales = db.People.Where(person => person.FullName.Contains(NameEntry)).Include("PrimaryContactPersonID")
                                       .SelectMany(x => x.Customers2).Include("CustomerID").SelectMany(x => x.Orders)
                                       //by using include we can merge in the OrderID teble
                                       .Include("OrderID").Include("CustomerID").SelectMany(x => x.Invoices)
                                       .Include("InvoiceID").SelectMany(x => x.InvoiceLines).Sum(x => x.ExtendedPrice),

                        GrossProfit = db.People.Where(person => person.FullName.Contains(NameEntry)).Include("PrimaryContactPersonID")
                                       .SelectMany(x => x.Customers2).Include("CustomerID").SelectMany(x => x.Orders)
                                       .Include("OrderID").Include("CustomerID").SelectMany(x => x.Invoices)
                                       .Include("InvoiceID").SelectMany(x => x.InvoiceLines).Sum(x => x.LineProfit),


                        //Items purchased details. A list of details about the top 10 most profitable items sold to the customer
                        ItemPurchaseSummary = Top10Items,

                        // Gets the Zip of the company 
                        CompanyZip = db.People.Where(person => person.FullName.Contains(NameEntry)).Include("PrimaryContactPersonID")
                                     .SelectMany(x => x.Customers2).Select(x => x.PostalPostalCode).First(),

                        // Gets the City of the company
                        CompanyCity = db.People.Where(person => person.FullName.Contains(NameEntry)).Include("PrimaryContactPersonID")
                                     .SelectMany(x => x.Customers2).Select(x => x.PostalAddressLine2).First(),
                        
                        // Gets the State of the company
                        CompanyState = db.People.Where(person => person.FullName.Contains(NameEntry)).Include("PrimaryContactPersonID")
                                     .SelectMany(x => x.Customers2).Include("City").Select(x => x.City).Include("StateProvinceID").Select(x => x.StateProvince)
                                     .Include("StateProvinceID").Select(x => x.StateProvinceCode).First()
                    }
                };

                ViewBag.Result = true;
                return View(Customers);
            }
        }
    }
}