using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using IIProjectClient.TrainServiceReference;
using IIProjectClient.Models;

namespace IIProjectClient.Controllers
{
    public class TrainController : Controller
    {

        TrainServiceClient localService = new TrainServiceClient();
        public TrainController()
        {
            
        }
        
        // GET: Train
        public ActionResult Index()
        {
            
            //Nedan fixar en dropdown lista att välja locations ifrån.
            List<string> allLocations = new List<string>();
            XElement test = new XElement("Locations", localService.GetAllLocations());
            foreach(var i in test.Descendants("Location").Elements("Name"))
            {
                allLocations.Add(i.Value);
            }

            ViewBag.DropDownValues = new SelectList(allLocations);
            localService.Close();
            return View();
        }
        [HttpPost]
        public ActionResult Index(string from, string to,string locationValue)
        {
            DateTime fromDate = new DateTime(Int32.Parse(localService.toDateTimeFormat(from)[0]), Int32.Parse(localService.toDateTimeFormat(from)[1]), Int32.Parse(localService.toDateTimeFormat(from)[2]));
            DateTime toDate = new DateTime(Int32.Parse(localService.toDateTimeFormat(to)[0]), Int32.Parse(localService.toDateTimeFormat(to)[1]), Int32.Parse(localService.toDateTimeFormat(to)[2]));

            XElement Locations = localService.GetAllLocations();
            
            string locationEPC = Locations.Descendants("Location").Where(p => p.Element("Name").Value == locationValue).FirstOrDefault().Element("Epc").Value;

            XElement searchResult = localService.GetPassageInfo(fromDate, toDate, locationEPC);

            List<string> allLocations = new List<string>();
            XElement test = new XElement("Locations", localService.GetAllLocations());
            foreach (var i in test.Descendants("Location").Elements("Name"))
            {
                allLocations.Add(i.Value);
            }

            ViewBag.DropDownValues = new SelectList(allLocations);

            localService.Close();
            return View();
           // return View("Index",searchResult);
        }
        
    }
}
