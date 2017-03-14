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
        
        
        public TrainController()
        {
            
        }
        
        // GET: Train
        public ActionResult Index()
        {
            TrainServiceClient localService = new TrainServiceClient();
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
        public ActionResult Index(string fromDateYear,string fromDateMonth,string fromDateDay, string toDateYear, string toDateMonth, string toDateDay,string locationValue)
        {
            TrainServiceClient localService = new TrainServiceClient();
            DateTime fromDate = new DateTime(Int32.Parse(fromDateYear), Int32.Parse(fromDateMonth), Int32.Parse(fromDateDay));
            DateTime toDate = new DateTime(Int32.Parse(toDateYear), Int32.Parse(toDateMonth), Int32.Parse(toDateDay));


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

            return View("Index",searchResult);
        }
        
    }
}
