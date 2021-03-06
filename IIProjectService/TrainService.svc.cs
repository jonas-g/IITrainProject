﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;
using IIProjectService.RemoteServiceReference;
using System.Web.Hosting;

namespace IIProjectService
{
    //Testkommentar

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class TrainService : ITrainService
    {
        EpcisEventServiceClient epcisEventService = new EpcisEventServiceClient();
        NamingServiceClient namingService = new NamingServiceClient();

        public TrainService()
        {
          // DateTime start = new DateTime(2011, 03, 20);
          // DateTime stop = new DateTime(2011, 03, 28);
          // string loc = "urn:epc:id:sgln:735999271.000.13";
          // XElement svar = GetPassageInfo(start, stop, loc);
        }

        public XElement GetPassageInfo(DateTime fromDate, DateTime toDate, string epcLocation)
        {
            var eventsData = GetEvents(fromDate, toDate, epcLocation);
            var locationData = GetLocation(epcLocation);

            XElement emptyVehicleData = new XElement("Fordonsindivid",
                new XElement("Fordonsnummer", "No data"),
                new XElement("Fordonsinnehavare",
                    new XElement("Foretag", "No data")),
                new XElement("UnderhallsansvarigtForetag",
                    new XElement("Foretag", "No data")),
                new XElement("FordonskategoriKodFullVardeEN", "No data"),
                new XElement("FordonsunderkategoriKodFullVardeEN", "No data"),
                new XElement("Godkannande",
                    new XElement("FordonsgodkannandeFullVardeEN", "No data"),
                    new XElement("GiltigtFrom", "No data"),
                    new XElement("GiltigtTom", "No data"))
                    );

            var query =
                new XElement("Passages",
                    from result in eventsData.Descendants("ObjectEvent")
                    let vehicleEPC = result.Descendants("epc").FirstOrDefault().Value
                    let locationEPC = result.Descendants("id").FirstOrDefault().Value
                    let time = result.Descendants("eventTime").FirstOrDefault().Value
                    let location = locationData.Descendants("Name").FirstOrDefault().Value
                    let vehicle = GetVehicle(vehicleEPC).Descendants("FordonsIndivid").Count() != 0 ? 
                        GetVehicle(vehicleEPC) : emptyVehicleData
                    let vehicleEVN = vehicle.Descendants("Fordonsnummer").FirstOrDefault().Value
                    let owner = vehicle.Descendants("Fordonsinnehavare").Elements("Foretag").FirstOrDefault().Value
                    let maintenance = vehicle.Descendants("UnderhallsansvarigtForetag").Elements("Foretag").FirstOrDefault().Value
                    let category = vehicle.Descendants("FordonskategoriKodFullVardeEN").FirstOrDefault().Value
                    let subcategory = vehicle.Descendants("FordonsunderkategoriKodFullVardeEN").FirstOrDefault().Value
                    let authMessage = vehicle.Descendants("FordonsgodkannandeFullVardeEN").FirstOrDefault().Value
                    let authFromDate = vehicle.Descendants("Godkannande").FirstOrDefault().Element("GiltigtFrom").Value
                    let authToDate = vehicle.Descendants("Godkannande").FirstOrDefault().Element("GiltigtTom") != null ? 
                        vehicle.Descendants("Godkannande").FirstOrDefault().Element("GiltigtTom").Value : "No data"
                    //let authBool = vehicle.Descendants("FordonsgodkannandeFullVardeEN").FirstOrDefault().Value.Contains("Permanent") ? true : false
                    select
                    new XElement("Passage",
                        new XElement("VehicleEPC", vehicleEPC),
                        new XElement("LocationEPC", locationEPC),
                        new XElement("Time", time),
                        new XElement("LocationName", location),
                        new XElement("Vehicle",
                            new XElement("EVN", vehicleEVN),
                            new XElement("Owner", owner),
                            new XElement("Maintenance", maintenance),
                            new XElement("Category", category),
                            new XElement("Subcategory", subcategory),
                            new XElement("AuthorisedMessage", authMessage),
                            new XElement("AuthorisedFromDate", authFromDate),
                            new XElement("AuthorisedToDate", authToDate)
                            )
                        )
                    );

            return query;
        }


        public void SaveToFile(XElement XDataPassages, XElement XDataServiceMsg)
        {
            var tempXml = new XElement("WebClientResponse", 
                          new XElement("ServiceMessage", XDataServiceMsg),
                          new XElement("VehiclePassages", XDataPassages));
                        
            tempXml.Save(HostingEnvironment.MapPath("/App_Data/OutputXML.xml"));
        }

        //*******************Metoder som hämtar från RemoteService (mest för att korta ner kod)******************

        //Bygga XElementet här eller senare(yield)?
        public XElement GetAllLocations()
        {
            return namingService.GetAllLocations();
        }

        //Bygga XElementet här eller senare(yield)?
        public IEnumerable<XElement> GetEvents(DateTime fromDate, DateTime toDate, string epcLocation)
        {
            try
            {
                return epcisEventService.GetEvents(fromDate,toDate,epcLocation);
            }
            catch (CommunicationException)
            {
                //CommunicationException???
                List<XElement> test = new List<XElement>();
                test.Add(new XElement("Error", "TMI"));
                return test;
                
            }
             
        }

        public XElement GetLocation(string epc)
        {
           return namingService.GetLocation(epc);
        }

        public XElement GetVehicle(string epc)
        {
          return  namingService.GetVehicle(epc);
        }

        public string[] toDateTimeFormat(string input)
        {
            return input.Split('/');            
        }
    }
}
