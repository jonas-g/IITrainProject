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
            this.Location = new Location();
            this.Vehicle = new Vehicle();
        }

        public static VehiclePassage fromXML(XElement xmlResponse)
        {
            VehiclePassage vehiclePassage = new VehiclePassage();
            Location location = vehiclePassage.Location;
            Vehicle vehicle = vehiclePassage.Vehicle;
            Authorisation authorisation = vehiclePassage.Vehicle.Auth;

            vehiclePassage.EventTime = DateTime.ParseExact(xmlResponse.Element("Time").Value, "yyyy-MM-ddTHH:mm:ss.fffffffZ", System.Globalization.CultureInfo.InvariantCulture);

            location.Name = xmlResponse.Element("LocationName").Value;
            location.EPC = xmlResponse.Element("LocationEPC").Value;

            var tempQuery = xmlResponse.Element("Vehicle");

            vehicle.EPC = xmlResponse.Element("VehicleEPC").Value;
            vehicle.EVN = tempQuery.Element("EVN").Value;
            vehicle.Owner = tempQuery.Element("Owner").Value;
            vehicle.Maintenance = tempQuery.Element("Maintenance").Value;
            vehicle.Category = tempQuery.Element("Category").Value;
            vehicle.Subcategory = tempQuery.Element("Subcategory").Value;
            authorisation.Message = tempQuery.Element("AuthorisedMessage").Value;
            authorisation.StartDate = tempQuery.Element("AuthorisedFromDate").Value;
            authorisation.EndDate = tempQuery.Element("AuthorisedToDate").Value;

            return vehiclePassage;
        }
    }
}