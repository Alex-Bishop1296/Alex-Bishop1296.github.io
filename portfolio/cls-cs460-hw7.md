# CS460 Homework 6

# Return to?
### [Code Repo](https://github.com/Alex-Bishop1296/Alex-Bishop1296.github.io) 
### [Home](../index.md) 
### [CS460 Assignments](cls-cs460.md) 

# Notes

# 1. [Setup] 
I started by creating an empty MVC project as per request, making sure to check the boxes for folders and core references for MVC. From here, I followed the following process.

# 2 - 5 [Coding/Content]
Here are my working notes:

WORKING NOTES START::

Made empty ASP.NET project with MVC folders and core references

Got giphy API key

Created a basic controller and view for TranslatorController and Index

Removed navbar and footer from layout

Made appSettings.config files outside of repo

In config file, put API key in enclosed in appSettings and add tags. Like so

```html
<appSettings>
  <add key="GiphyAPIKey" value="MY API KEY HERE"/>
</appSettings>
```

In my projects Web.config file, I added the argument 

```html
file = “../../appSettings.config”
```

To my appSettings tag to ensure it would look outside of the project for the API key.

From here we started on the (control script) JS file, adding in the argument for keypress, and running our method for conversion check on each space entered, we split the string of words into a list of words and take the last one. If the last one is a noun or verb (via GITHUB list of nouns or verbs check) we run an argument from our new controller for the API (I called it words). From here I made the new controller, and began to set up the pathing for retrieving the API. I also edited the route config to practice custom routing, using our new controller and Giphy conversion method within it.

Back on the Words controller, we needed to open NuGet and instal Newtonsoft.Json to parse the json object from Giphy.

Then we finished up the coding inside the Words controller to return the url of the giphy gif.

From there we completed our control script js file, adding success and failure methods to either display the gif via append or throw an error.

Then in the index file for our view, we added the section for appended words and gifs to display in.

After that we added a render section for the js code to our index file and the renderSection to our renderbody in layout.

Next we had to create the database for logging. I started by creating a mdf file for the database. I also created up and down files for the table. Then I had to connect to the database from Visual Studio by using the server explorer, connecting to the local db, then selecting the database branch for our project. I then installed a Nugget installer to get the entity framework. I ran the up script, then I generated a model for our table from the up script. I then had to change my Web.config file to make sure the connection string points at the database. 

Next we had to generate a DAL folder and made a context for our model. Then, after including the model and context in our controller, we connect the necessary inputs on the model each call and log it to the logger and save changes.

END WORKING NOTES

Let me now break down the important steps with code and details

I started by generating the API key from Giphy for importing it into my project. This would be used to actually get gifs from the Giphy API to use in my translator. Then I created a basic controller and view for my controller for the only webpage in this project, removing many defaults from the layout and css to get the look I wanted. My code for index looks like this:

```html
@{
    ViewBag.Title = "Index";
}

<div style="text-align: center">
    <h2>Internet Language Translator</h2>
</div>
<br />
<!--Search bar that sends words to our JS and controller-->
@Html.TextBox("ToTranslate", null , new { @class = "form-control", placeholder = "Start your message here..." })  

<!--Here we hold all translated information-->
<div class="translation">

</div>
<!--Here we have the section to actually generate the scripts code four our translation box-->
@section scriptSection{
     <script src="~/Scripts/jquery-3.3.1.min.js"></script>   
     <script src="~/Scripts/ControlScript.js"></script>
}
```

Simple code here, a textbox razor html element for the form and sending to the controller/js script, a divider for the translation, and the a section for rendering the script information with a link to our control script. We have a render section in our layout file to actually make sure that scriptSection gets rendered properly. Like mentioned in my working notes, I also linked my API key to my project from outside the project after this. Next came the controller script, the logic works as follows:

```js
$('#ToTranslate').keypress(function (e) {

    // Store the string from input field into value
    var word = $('#ToTranslate').val();

    //This is for checking the browser type ascii character for zero
    // 0 works is for mozilla and 32 for other browsers
    if (e.keyCode == 0 || e.keyCode == 32) {
        // Log a space has come in
        console.log("Space");
        // split up the words of the input
        var prevWord = word.toString().split(" ");

        // get most recent word
        var recWord = prevWord[prevWord.length - 1];
        console.log("Word coming in is  " + recWord); // If the most recent word is a noun or verb

        // If most recent word is a noun or verb (interesting word)
        if (nouns.includes(recWord.toLowerCase()) || verbs.includes(recWord.toLowerCase())) {
            //Create the source path url
            var source = "/Word/RetrieveGiphy/" + recWord

            //Debug
            console.log("source");

            // Sending the recWrod to the controller via the source URL, executing a method based on success or failure
            $.ajax({
                method: "GET",
                dataType: "json",
                url: source,
                success: successGif,
                error: errorAjax
            })
        }
        //Append non-interesting words to the translator
        else {
            $(".translation").append(recWord + " ");
        }
    }
});
```

Excluded from this text block is the sections for my successGif, errorAjax, and arrays for nouns and verbs. The general logic here is to look at each keypress, if it is a space we then parse the textbox for the lastword, with that last or recent word we check if it is interesting or not based on if it is a noun or verb. If it is interesting we generate a path and use ajax to send to our controller. If it is not interesting we just append it to the translation block. Before going on to the Controller for this logic, I had to adjust the routing path as per the requirement, here is the code for that inside route config:

```csharp
            //As per the requirements for this assignment we made a new route in route config
            routes.MapRoute(
               name: "RetrieveGiphy",
               // url is name of controller, method used, and then name of string taken in
               url: "{controller}/{action}/{word}",
               defaults: new { controller = "Word", action = "RetrieveGiphy" }
            );
```

Now, back on the topic of the controller, the logic looks as follows:

```csharp 
    public class WordController : Controller
    {
        private LoggerContext db = new LoggerContext();

        /// <summary>
        /// Controller for placing images in the tranlation divider after the ControlScript.js sends them
        /// </summary>
        /// <param name="word">word that is being translated with the giphy API</param>
        /// <returns>The JSON of the gif url for the ControlScript.js to use</returns>
        public JsonResult RetrieveGiphy(string word)
        {
            // Start by getting Giphy API
            string key = System.Configuration.ConfigurationManager.AppSettings["GiphyAPIKey"];

            //Debug messages
            System.Diagnostics.Debug.WriteLine("API Key:" + key);
            System.Diagnostics.Debug.WriteLine("search word:" + word);

            //Using API key, get the webadress of a Giphy gif
            string website = "https://api.giphy.com/v1/stickers/translate?api_key=" + key + "&s=" + word;


            // Create a website request for giphy
            WebRequest request = WebRequest.Create(website);
            request.ContentType = "application/json; charset=utf-8";
            //assign the response from the website, this will be a JSON object
            var response = (HttpWebResponse)request.GetResponse();
            string jsonWord;
            // Stream reader for taking the JSON out of the response
            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                //Place JSON in our string
                jsonWord = stream.ReadToEnd();
                //Close the stream
                stream.Close();
            }

            // We will use these for parsing the JSON object
            string jsonUrl, jsonData;

            // Here we are pulling all the values from the json file for the data field, then inside that field we are grabing the url field
            jsonData = JObject.Parse(jsonWord)["data"].ToString();
            jsonUrl = JObject.Parse(jsonData)["embed_url"].ToString();

            //Debug messages
            System.Diagnostics.Debug.WriteLine("jsonWord:" + jsonWord);
            System.Diagnostics.Debug.WriteLine("jsonURl" + jsonUrl);


            // put URL of the gif inside a new json object
            var gifUrl = new
            {
                embed_url = jsonUrl
            };

            //Database extra stuff goes here
            var ip = Request.UserHostAddress;
            var agent = Request.Browser.Type;
            var newLog = new Log
            {
                Date = DateTime.Now,
                Word = word,
                GiphyURL = gifUrl.embed_url,
                IP = ip,
                Browser_Agent = agent
            };
            db.Log.Add(newLog);
            db.SaveChanges();


            return Json(gifUrl, JsonRequestBehavior.AllowGet);
        }
    }
```

What is going on here is quite a few things. We get the API key from outside the project and attach it to a string to create a proper URL. Next we create a web request with that url and get a response as a json object. With this object, we extract the URL from the datafield (has the actual path to the gif we can use) and place it in a string. We then use that string for a new json object and give it the url. Then we log all of our current information into our database, save the changes, and pass our json object back to the script. From our script we display the gif on our page.

Here is a link to the demo of my project:
[Demo Video Link](https://youtu.be/USuxCyvT-t0)
