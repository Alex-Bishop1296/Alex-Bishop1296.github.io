namespace AuctionHouse.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            Bids = new HashSet<Bid>();
        }

        [Display(Name ="Item ID")]
        public int ItemID { get; set; }

        [Display(Name = "Item Name")]
        [Required]
        [StringLength(128)]
        public string ItemName { get; set; }

        [Display(Name = "Item Description")]
        [Required]
        [StringLength(256)]
        public string ItemDescription { get; set; }

        public int SellerID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bid> Bids { get; set; }

        [Display(Name = "Seller")]
        public virtual Seller Seller { get; set; }
    }
}
