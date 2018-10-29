using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// Allows me to control the forms for the database via requirements
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
        [MaxLength(64)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        // last name of the tenant
        [Required]
        [MaxLength(128)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        // phone number of the tenant
        [Required]
        [Phone]
        [MaxLength(14)]
        [RegularExpression(@"^\d{1}-\d{3}-\d{3}-\d{4}$", ErrorMessage = "Please format to 1-999-999-9999")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        // apartment name the tenant is in
        [Required]
        [MaxLength(64)]
        [DisplayName("Building Name")]
        public string ApartmentName { get; set; }

        // unit of the apartement the tenant is in
        [Required]
        [MaxLength(64)]
        [DisplayName("Room Number")]
        public int UnitNumber { get; set; }

        // the details of the request of the tenant
        [Required]
        [MaxLength(1000)]
        [DisplayName("Request Description")]
        public string RequestDetails { get; set; }

        // the given permission to actually do the request
        public bool Permission { get; set; }

        // timestap of request paired to datetime value
        private DateTime date = DateTime.Now;
        [Required]
        [DisplayName("Time of Submission")]
        public DateTime SubmitTime
        {
            get { return date; }
            set { date = value; }
        }

    }
}