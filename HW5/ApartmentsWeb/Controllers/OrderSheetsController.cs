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

        // POST: OrderSheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
