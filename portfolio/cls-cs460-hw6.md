# CS460 Homework 6

# Return to?
### [Code Repo](https://github.com/Alex-Bishop1296/Alex-Bishop1296.github.io) 
### [Home](../index.md) 
### [CS460 Assignments](cls-cs460.md) 

# Notes

# 1 & 2.[Setup] 
To start off with this assignment, I had to download SQL Server Management Studio and it's associated app, as well as LINQ pad for testing the queries on the database. Then I branched from master for this assignment (promptly hw-six branch) as a feature. From there I created a empty project. Next, I had to download the backup of the database and load it into my project. I did this by loading the backup on SQL Server Management Studio into my project. Then I used Nuget to load in Microsoft.SqlServer.Type into the project, as well as adding the following two lines to my Global.asax.cs file:

```csharp
// For Spatial types, i.e. DbGeography
    SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
// This next line is a fix that came from: https://stackoverflow.com/questions/13174197/microsoft-sqlserver-types-version-10-or-higher-could-not-be-found-on-azure/40166192#40166192
    SqlProviderServices.SqlServerTypesAssemblyName = typeof(SqlGeography).Assembly.FullName;
```

This allowed the use of DbGeography. I made sure to delete most of the default views and controller so I could work from scratch on this project. With the backup file loaded in, I also had access to all the models of the database as well.

# 3 & 4. [Coding and Content]
Starting this project out, I had to create a model which would work for feature 1 and eventually 2 and 3 (I did complete the extra credit so I will go into detail about it.) I created a subfolder in the Models folder called View Models. From here, I started by creating the PersonVM.cs file, which would be my primary file for interfacing witht the database. I made sure to include the Systems.Collections.Generic so I could use lists and collections. The code looks like this:

```csharp
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

        // Now we have the additional details for the second feature set

        //Customer Company Details
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyFax { get; set; }
        public string CompanyWebsite { get; set; }
        public DateTime CompanyValidFrom { get; set; }

        //Purchase History Details
        public double Orders { get; set; }
        public decimal GrossSales { get; set; }
        public decimal GrossProfit { get; set; }

        //Items Purchased Details
        public List<ItemPurchase> ItemPurchaseSummary { get; set; }

        // Location
        public string CompanyZip { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        // Nullable is used as required from the database
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
```

We can see alot just by looking at this code, but its actually quite simple. We have various fields that will be used by the controller to to map data from the database. You notice we also have a list of Item Purchase objects. This is another model in the View Models folder. I won't show it as it will be made apparent later. From here I had a solid basis to get what I needed out of the database. Next, I would need a controller for the Search and Details pages.

The first part of the controller was simply to display the search page, the code looks like this:

```csharp
        /// <summary>
        /// Return the view of the search with results
        /// </summary>
        /// <param name="input">name searched to get results</param>
        /// <returns>either blank search form or search form with names view</returns>
        [HttpGet]
        public ActionResult Search(string input)
        {

            // If the search string is empty return basic view
            if (input == "" || input == null)
            {
                return View();
            }

            // Run the search
            else
            {
                List<PersonVM> Person = db.People
                                        .Where(n => n.FullName.Contains(input))
                                        .Select(n => new PersonVM
                                        {
                                            FullName = n.FullName
                                        }).ToList();
                // Allow the new customer list to be viewed via if in html
                ViewBag.Result = true;
                return View(Person);
            }
        }
```

This takes a string taken from the view, input, is checked for validity (ie a searchable input) if it is not searchable we return to the page. If we can search, we make a list of objects based on our model, her we search our database in people where the name contains the input and then select those names that are valid, finally putting them into a list. This gives us all names that macth our search. We make a viewbag element called Result and set it to true, then we return out list (called Person) to the view. The view that processes this information looks like so:

```html
<!-- Calling the database model to allow the rendering of information-->
@model IEnumerable<BigData.Models.ViewModels.PersonVM>
@{
    ViewBag.Title = "Search";
}

<div class="content">
<div class="row">
    <!-- Search Bar-->
    <div class="col-md-12">
        <h1 class="text-center">Search</h1>
        <!-- Search form -->
        @using (Html.BeginForm("Search", "Query", FormMethod.Get))
        {
        <div class="containerClass">
            <input class="form-control" type="text" name="input" placeholder="Search by client name" />
            <button type="submit" class="btn btn-primary searchIcon sumbitClass">Search</button>
        </div>
        }
    </div>
</div>

<!--Results Form-->
@if (ViewBag.Result == true)
{
    <table>
        @foreach (var item in Model)
        {
            <tr>
                <td>
                    <!--Creates each link to the details page based on the names searched-->
                    <h4>@Html.ActionLink(item.FullName, "Details", "Query", new { NameEntry = item.FullName }, null)</h4>
                </td>
            </tr>
        }
    </table>
}
</div>
```

We open this page up with an enumerable for use of foreach on our datamodel, as well as access to each element for printing. Then we have our form element inside some bootstrap, allowing the submit and text entry to go directly into our controller. Next we have out results in a if that only displays when our viewbag triggers it. In this if we have a foreach go through our list and print all of the valid names from the search. Since we know how our search works, time to see how we display the details.

Here I am going to break down the code for the details view controller. Since we have a lot of queries that are used, I will keep the structure while omitting repeatative details. Let's start by looking at a little of it:

```csharp
/// <summary>
        /// Return the results a search in detail based on given name
        /// </summary>
        /// <param name="NameEntry">Exact name of the person that was searched for</param>
        /// <returns>view with details of name given, more or less depending if the customer is with a company</returns>
        [HttpGet]
        public ActionResult Details(string NameEntry)
        {
            //Check if the input is valid
            if (NameEntry == null || NameEntry == "")
            {
                // Redirect to search page if URL is edited
                return RedirectToAction("Search");
            }
            // Get the base details of the listed search of an person in the system
            // This search here is just looking at the people table in the database where the fullname is equal to the one we were given by the parameter
            List<PersonVM> IndividualDetails = db.People
                                                   .Where(x => x.FullName.Equals(NameEntry))
                                                   .Select(x => new PersonVM
                                                   {
                                                       FullName = x.FullName,
                                                       PreferredName = x.PreferredName,
                                                       PhoneNumber = x.PhoneNumber,
                                                       FaxNumber = x.FaxNumber,
                                                       EmailAddress = x.EmailAddress,
                                                       ValidFrom = x.ValidFrom,
                                                   }).ToList();
```

Here we have the details for feature 1.