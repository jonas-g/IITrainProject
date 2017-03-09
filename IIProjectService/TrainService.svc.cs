using System;
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

        public XElement GetPassageInfo(DateTime fromDate, DateTime toDate, string epcLocation)
        {
            var eventsData = GetEvents(fromDate, toDate, epcLocation);
            var locationData = GetLocation(epcLocation);
           // var vehicleData = Någont som via evensData hämtar vagnens id....


            throw new NotImplementedException();
        }


        public void SaveToFile(XElement XData)
        {
            XData.Save(HostingEnvironment.MapPath("/App_Data/OutputXML.xml"));
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
             return epcisEventService.GetEvents(fromDate,toDate,epcLocation);
        }

        public XElement GetLocation(string epc)
        {
           return namingService.GetLocation(epc);
        }

        public XElement GetVehicle(string epc)
        {
          return  namingService.GetVehicle(epc);
        }

    }
}
