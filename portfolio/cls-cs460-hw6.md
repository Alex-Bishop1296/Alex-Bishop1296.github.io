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

Here we have the details for feature 1. We take in a string just like our search, and do the same check for valid entry, if the validity check fails, we redirect the user back to the homepage, helping us deal with direct intput into the url. If we have valid input, we do that same query from our basic search, however, we make sure the name is exactly the one given (as that is what we pass to this view) and we get multiple elements assigned to the modeled object for PersonVM. After this, I do another search for the feature 2, or customer details, if we don't find those details, in an if else, we exit with the feature one details, ie the searched person is not a customer. If we do find those details, we start assigning more fields to the model. One of the notable assignments is as follows:

```csharp
                //List object to store the top 10 items sold to the customer
                List<ItemPurchase> Top10Items = new List<ItemPurchase>();
                //Intializes a list of Item Purchase class that contains the details for the top 10 items sold to the customer.
                for (int i = 0; i < 10; i++)
                {
                    Top10Items.Add(new ItemPurchase
                    {
                        StockItemID = ItemDetails.ElementAt(i).StockItemID,
                        ItemDescription = ItemDetails.ElementAt(i).Description,
                        LineProfit = ItemDetails.ElementAt(i).LineProfit,
                        SalesPerson = SalesMen.ElementAt(i).FullName
                    });
                }
```

This Top10Items list is used directly by the model for displaying the top 10 items sold to the customer. We make a list of our itempurchase objects which we later assign the details for each object in the database info. The majority of the rest of this code in our controller method is just assigning fields from the model with a query that starts at the customer's name and goes deeper into the database until we get the data we need. One thing to note is that we grab the latitude and longitude for use in displaying the company location on a map using OpenStreetMap and Leaflet. Here is what grabbing those looks like:

```csharp
                        // Gets the latitude of the company
                        Latitude = db.People.Where(person => person.FullName.Contains(NameEntry)).Include("PrimaryContactPersonID")
                                     .SelectMany(x => x.Customers2)
                                     .Select(x => x.City)
                                     .Include("City")
                                     .Select(x => x.Location.Latitude).First(),

                        // Gets the longitude of the company
                        Longitude = db.People.Where(person => person.FullName.Contains(NameEntry)).Include("PrimaryContactPersonID")
                                     .SelectMany(x => x.Customers2)
                                     .Select(x => x.City)
                                     .Include("City")
                                     .Select(x => x.Location.Longitude).First()
```

Notice that we going into the "PrimaryContactPersonID" table via person.FullName with People's foreign key. Then we get another include in the City table to final grab what we need. With all of our data extracted we can look at the cshtml for some of our display:

```html
<!-- Calling the database model to allow the rendering of information-->
@model IEnumerable<BigData.Models.ViewModels.PersonVM>
@{
    ViewBag.Title = "Details";
}

<!--Used to generate map with API-->
<link rel="stylesheet" href="https://unpkg.com/leaflet@1.3.4/dist/leaflet.css"
      integrity="sha512-puBpdR0798OZvTTbP4A8Ix/l+A4dHDD0DGqYW6RQ+9jxkRFclaxxQb/SJAWZfWAkuyeQUytO7+7N4QKrDh+drA=="
      crossorigin="" />
<script src="https://unpkg.com/leaflet@1.3.4/dist/leaflet.js"
        integrity="sha512-nMMmRyTVoLYqjP9hrbed9S+FzjZHW5gY1TWCHA5ckwXZBadntCNs8kEqAWdrb9O7rxbCaA4lKTIWjDXZxflOcA=="
        crossorigin=""></script>


<div class="content">
    <div class="row">
        <!-- Large Column Left-->
        <div class="col-md-6">
            <!--First Name Area-->
            <div class="row">
                <div class="col-md-8">
                    <h2><b>@Model.FirstOrDefault().FullName</b></h2>
                    <hr />
                </div>
            </div>
```

Here we load in the data model, then we generate the map js and css from the API (more on that in a bit). Finally we start our left col-md-6 (the left half of the page). Here we grab the FullName from the model. This step repeats for each and every item in the model, doing a foreach for the purchases as well like we did in the search results. We only display the feature 2 results if we pass the results = true like on our search too. Our map code looks like this:

```html
            <div>
                <h3>Company Location:</h3>
                <!--Area-->
                <p>@Model.FirstOrDefault().CompanyCity, @Model.FirstOrDefault().CompanyState, @Model.FirstOrDefault().CompanyZip</p>

                <div id="mapid"></div>
                <!-- Script here actual intializes the longitude and latitude-->
                <script>
                    // center of the map
                    var center = [@Model.FirstOrDefault().Latitude,@Model.FirstOrDefault().Longitude];

                    // Create the map
                    var map = L.map('mapid').setView(center, 13);

                    // Set up the OSM layer
                    L.tileLayer(
                        'http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                            maxZoom: 18
                        }).addTo(map);

                    // add a marker in the given location
                    L.marker(center).addTo(map);
                </script>

            </div>
```

We display the location of the company via text fields like earlier, then we place a mapid div. This is actually where our map is going to be displayed. Next, we start a script for placing the map via JS. We make a centerpoint based on our Lat and Longit, we set the veiw of the map view var map, then finally we loading in the openstreetmap tilelayer or actual map imaging. Then we add our marker to the map and end the script. With this we can actually display all of the named customer's results. I will demo these features all in the video below
