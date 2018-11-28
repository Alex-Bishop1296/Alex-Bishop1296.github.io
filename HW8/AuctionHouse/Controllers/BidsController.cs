using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuctionHouse.DAL;
using AuctionHouse.Models;

namespace AuctionHouse.Controllers
{
    public class BidsController : Controller
    {

        private AuctionContext db = new AuctionContext();
        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName");
            ViewBag.BuyerID = new SelectList(db.Buyers, "BuyerID", "BuyerName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BidID,ItemID,BuyerID,Price,Timestamp")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                // control to make sure only bids higher than current bid can be made
                //Item item = db.Items.Where(i => i.ItemID.Equals(bid.ItemID)).FirstOrDefault();
                //Bid recent = item.Bids.LastOrDefault();

                //if(recent == null || bid.Price > recent.Price)
                //{
                    db.Bids.Add(bid);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
            //    }

            //    else
            //    {
            //        ViewBag.Buyer = new SelectList(db.Buyers, "BuyerID", "BuyerName", bid.BuyerID);
            //        ViewBag.Item = new SelectList(db.Items, "ItemID", "ItemName", bid.ItemID);
            //        ModelState.AddModelError("Price", "A greater bid already exists. Please bid a value greater than: " + recent.Price);
            //        return View(bid);
            //    }
            }
            ViewBag.BuyerID = new SelectList(db.Buyers, "BuyerID", "BuyerName", bid.BuyerID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName", bid.ItemID);
            return View(bid);
        }
    }
}