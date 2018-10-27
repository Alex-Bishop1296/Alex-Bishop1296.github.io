using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApartmentsWeb.DAL;
using ApartmentsWeb.Models;

namespace ApartmentsWeb.Controllers
{
    public class OrderSheetsController : Controller
    {
        private OrderContext db = new OrderContext();

        /// <summary>
        /// GET the view of all of the items on the OrderSheet table by SubmitTime
        /// </summary>
        /// <returns>View of table in webpage</returns>
        public ActionResult Index()
        {
            var list = db.OrderSheets.ToList();
            var orderedList = list.OrderBy(item => item.SubmitTime);
            return View(orderedList);
        }

        /// <summary>
        /// Show the user via GET the view for submiting a order
        /// </summary>
        /// <returns>view of order form</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Uses GET-POST-Retrieve to add elements to the table via create form
        /// </summary>
        /// <param name="orderSheet">The entry to add to OrderSheets table</param>
        /// <returns>The view of the form for submiting work orders</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,PhoneNumber,ApartmentName,UnitNumber,RequestDetails,Permission,SubmitTime")] OrderSheet orderSheet)
        {
            if (ModelState.IsValid)
            {
                db.OrderSheets.Add(orderSheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderSheet);
        }

        /// <summary>
        /// Protected Disposal method
        /// </summary>
        /// <param name="disposing">To run method or not via bool</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
