using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Added using for context and model
using AuctionHouse.DAL;
using AuctionHouse.Models;

namespace AuctionHouse.Controllers
{
    public class BidsController : Controller
    {
        //Context
        private AuctionContext db = new AuctionContext();
        // GET: Items/Create
        /// <summary>
        /// Create a view for the bid placer, pass it the information for the buyer id and buyer names
        /// </summary>
        /// <returns>Return view with lists of buyers and items to pick from</returns>
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName");
            ViewBag.BuyerID = new SelectList(db.Buyers, "BuyerID", "BuyerName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Pass the details of the bid to the database
        /// </summary>
        /// <param name="bid">The bid database entry object</param>
        /// <returns>Return the view with the bid or just take the viewer back to index home</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BidID,ItemID,BuyerID,Price,Timestamp")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                    db.Bids.Add(bid);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
            }
            ViewBag.BuyerID = new SelectList(db.Buyers, "BuyerID", "BuyerName", bid.BuyerID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName", bid.ItemID);
            return View(bid);
        }
    }
}