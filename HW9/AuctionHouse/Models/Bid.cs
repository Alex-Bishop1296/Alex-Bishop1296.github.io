namespace AuctionHouse.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bid
    {
        [Display(Name ="Bid ID")]
        public int BidID { get; set; }

        [Display(Name = "Item ID")]
        public int ItemID { get; set; }

        [Display(Name = "Buyer ID")]
        public int BuyerID { get; set; }

        public decimal Price { get; set; }

        private DateTime date = DateTime.Now;
        [Display(Name = "Submission Time")]
        public DateTime Timestamp
        {
            get { return date; }
            set { date = value; }
        }

        public virtual Buyer Buyer { get; set; }

        public virtual Item Item { get; set; }
    }
}
