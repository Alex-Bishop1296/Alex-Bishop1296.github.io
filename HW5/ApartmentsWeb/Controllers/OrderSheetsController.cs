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

        // GET: OrderSheets
        public ActionResult Index()
        {
            return View(db.OrderSheets.ToList());
        }

        // GET: OrderSheets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderSheet orderSheet = db.OrderSheets.Find(id);
            if (orderSheet == null)
            {
                return HttpNotFound();
            }
            return View(orderSheet);
        }

        // GET: OrderSheets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderSheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,PhoneNumber,ApartmentName,UnitNumber,RequestDetails,Permission")] OrderSheet orderSheet)
        {
            if (ModelState.IsValid)
            {
                db.OrderSheets.Add(orderSheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderSheet);
        }

        // GET: OrderSheets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderSheet orderSheet = db.OrderSheets.Find(id);
            if (orderSheet == null)
            {
                return HttpNotFound();
            }
            return View(orderSheet);
        }

        // POST: OrderSheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,PhoneNumber,ApartmentName,UnitNumber,RequestDetails,Permission")] OrderSheet orderSheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderSheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderSheet);
        }

        // GET: OrderSheets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderSheet orderSheet = db.OrderSheets.Find(id);
            if (orderSheet == null)
            {
                return HttpNotFound();
            }
            return View(orderSheet);
        }

        // POST: OrderSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderSheet orderSheet = db.OrderSheets.Find(id);
            db.OrderSheets.Remove(orderSheet);
            db.SaveChanges();
            return RedirectToAction("Index");
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
