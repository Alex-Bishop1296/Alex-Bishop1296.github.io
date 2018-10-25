using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// Allows me to control the forms for the database via requirements
using System.ComponentModel.DataAnnotations;

namespace ApartmentsWeb.Models
{
    /// <summary>
    /// Contains the fields for an order sheet in the Ordersheet table
    /// </summary>
    public class OrderSheet
    {
        // Required fields on the form

        [Key]
        public int ID { get; set; }

        // first name of tenant
        [Required]
        public string FirstName { get; set; }

        // last name of the tenant
        [Required]
        public string LastName { get; set; }

        // phone number of the tenant
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        // apartment name the tenant is in
        [Required]
        public string ApartmentName { get; set; }

        // unit of the apartement the tenant is in
        [Required]
        public int UnitNumber { get; set; }

        // the details of the request of the tenant
        [Required]
        public string RequestDetails { get; set; }

        // the given permission to actually do the request
        public bool Permission { get; set; }

        // timestap of request paired to datetime value
        private DateTime date = DateTime.Now;
        [Required]
        private DateTime SubmitTime
        {
            get { return date; }
            set { date = value; }
        }

    }
}