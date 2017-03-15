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
            Users users = new Users();

            ViewBag.DropDownValues = new SelectList(GetLocationList());

            if (users.userList.Contains(user))
            {
                TrainServiceClient localService = new TrainServiceClient();

                DateTime fromDate = new DateTime(Int32.Parse(localService.toDateTimeFormat(from)[0]), 
                    Int32.Parse(localService.toDateTimeFormat(from)[1]), 
                    Int32.Parse(localService.toDateTimeFormat(from)[2]));
                DateTime toDate = new DateTime(Int32.Parse(localService.toDateTimeFormat(to)[0]), 
                    Int32.Parse(localService.toDateTimeFormat(to)[1]), 
                    Int32.Parse(localService.toDateTimeFormat(to)[2]));

                XElement Locations = localService.GetAllLocations();

                string locationEPC = Locations.Descendants("Location").Where(p => p.Element("Name").Value == locationValue).FirstOrDefault().Element("Epc").Value;

                XElement searchResult = localService.GetPassageInfo(fromDate, toDate, locationEPC);

                localService.SaveToFile(searchResult);

                localService.Close();

                List<VehiclePassage> queryPassages = new List<VehiclePassage>();

                foreach (var p in searchResult.Descendants("Passage"))
                {
                    queryPassages.Add(VehiclePassage.fromXML(p));
                }

                //return View();
                return View("Index", queryPassages);
            }
            else
            {
                ViewBag.Message = "Invalid username";
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
