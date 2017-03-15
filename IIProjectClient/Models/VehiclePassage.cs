using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace IIProjectClient.Models
{
    public class VehiclePassage
    {
        public Vehicle Vehicle { get; set; }
        public Location Location { get; set; }
        public DateTime EventTime { get; set; }

        public VehiclePassage()
       
        {
            XElement XXXXXXX = new XElement("", "");
        }

        public static void fromXML(XElement xmlResponse)
        {
            VehiclePassage vehiclePassage = new VehiclePassage();
            Location location = new Location();
            Vehicle vehicle = new Vehicle();

            vehiclePassage.EventTime = DateTime.Now;// String behöver göras till datetime xmlResponse.Element("Time").Value;

            location.Name = xmlResponse.Element("LocationName").Value;
            location.EPC = xmlResponse.Element("LocationEPC").Value;

            var tempQuery = xmlResponse.Element("Vehicle");

            vehicle.EPC = xmlResponse.Element("VehicleEPC").Value;
            vehicle.EVN =Int32.Parse( tempQuery.Element("EVN").Value);
            vehicle.Owner = tempQuery.Element("Owner").Value;
            vehicle.Maintenance = tempQuery.Element("Maintenance").Value;
            vehicle.Category = tempQuery.Element("Category").Value;
            vehicle.Subcategory = tempQuery.Element("Subcategory").Value;
            //evetuell info om auth(godkännande)


        }
    }
}