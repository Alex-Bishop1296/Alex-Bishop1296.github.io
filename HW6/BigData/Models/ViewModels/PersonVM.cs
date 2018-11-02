﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigData.Models.ViewModels
{
    /// <summary>
    /// Here we store items from our Person model that will be used
    /// directly by our view model, we do this so we only access the
    /// items from the database that we need.
    /// </summary>
    public class PersonVM
    {
        // The reason we don't grab the annotations is because that
        // we do not do any setting of new values, thus we avoid
        // grabbing unecessary data

        // Basic elements, no explaination needed
        public string FullName { get; set; }
        public string PreferredName { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }

        // used for the The date they became a customer, member or employee
        public DateTime ValidFrom { get; set; }

        // We will hard code getting the photo, so we don't here
    }
}