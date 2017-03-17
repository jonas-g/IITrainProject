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
            ViewBag.DropDownValues = new SelectList(GetLocationList());

            return View();
        }
        [HttpPost]
        public ActionResult Index(string from, string to, string locationValue, string user)
        {
            TrainServiceClient localService = new TrainServiceClient();
            Users users = new Users();
            ServiceMessage message = new ServiceMessage();
            message.SentArgument = "From: " + from + ", To:" + to + ", At:" + locationValue;
            message.CallManager = user;
            message.AnswerTime = DateTime.Now;

            ViewBag.DropDownValues = new SelectList(GetLocationList());

            if (users.userList.Contains(user))
            {
                message.AnswerCode = 1;
                message.Message = "Query successful";

                DateTime fromDate = new DateTime(Int32.Parse(localService.toDateTimeFormat(from)[0]), 
                    Int32.Parse(localService.toDateTimeFormat(from)[1]), 
                    Int32.Parse(localService.toDateTimeFormat(from)[2]));
                DateTime toDate = new DateTime(Int32.Parse(localService.toDateTimeFormat(to)[0]), 
                    Int32.Parse(localService.toDateTimeFormat(to)[1]), 
                    Int32.Parse(localService.toDateTimeFormat(to)[2]));

                XElement Locations = localService.GetAllLocations();

                string locationEPC = Locations.Descendants("Location").Where(p => p.Element("Name").Value == locationValue).FirstOrDefault().Element("Epc").Value;

                XElement searchResult = localService.GetPassageInfo(fromDate, toDate, locationEPC);

                if (searchResult.Descendants("Error").Count() == 0)
                {
                    List<VehiclePassage> queryPassages = new List<VehiclePassage>();

                    foreach (var p in searchResult.Descendants("Passage"))
                    {
                        queryPassages.Add(VehiclePassage.fromXML(p));
                    }
                    ViewData["ServiceMessage"] = message;
                    localService.SaveToFile(searchResult, message.toXML());
                    return View("Index", queryPassages);
                }
                else
                {
                    message.AnswerCode = 2;
                    message.Message = "Error";
                    ViewData["ServiceMessage"] = message;

                    return View();
                }
            }

            else
            {
                message.AnswerCode = 3;
                message.Message = "Autentisering misslyckades";
                ViewData["ServiceMessage"] = message;

                return View();
            }
        }

        private List<string> GetLocationList()
        {
            TrainServiceClient localService = new TrainServiceClient();

            List<string> allLocations = new List<string>();
            XElement test = new XElement("Locations", localService.GetAllLocations());
            foreach (var i in test.Descendants("Location").Elements("Name"))
            {
                allLocations.Add(i.Value);
            }
            localService.Close();
            return allLocations;
        }
    }
}
