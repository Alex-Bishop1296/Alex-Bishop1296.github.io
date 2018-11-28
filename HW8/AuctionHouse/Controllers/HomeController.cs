using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuctionHouse.Models;
using AuctionHouse.DAL;

namespace AuctionHouse.Controllers
{
    public class HomeController : Controller
    {
        //Context
        private AuctionContext db = new AuctionContext();


        // GET: Home
        public ActionResult Index()
        {
            var list = db.Bids.ToList();
            var orderedList = list.OrderByDescending(x => x.Timestamp).Take(10);
            return View(orderedList);
        }
    }
}