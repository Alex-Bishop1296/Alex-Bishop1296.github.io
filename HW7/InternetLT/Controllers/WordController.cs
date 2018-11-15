using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
// For parsing the Json response from Giphy
using Newtonsoft.Json.Linq;
using InternetLT.DAL;
using InternetLT.Models;

namespace InternetLT.Controllers
{
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
}