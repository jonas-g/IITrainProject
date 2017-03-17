using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace IIProjectClient.Models
{
    //Klass för tjänstemeddelande
    public class ServiceMessage
    {
        public int AnswerCode { get; set; }
        public string Message { get; set; }
        public string ServiceManager { get; set; }
        public string NameVersion { get; set; }
        public DateTime AnswerTime { get; set; }
        public string CallManager { get; set; }
        public string SentArgument { get; set; }

        public ServiceMessage()
        {
            this.ServiceManager = "Tågsammansättningsinformationstjänst.se";
            this.NameVersion = "Tågsammansättningsinformationstjänsteappen v.1.1.1.1";
        }

        public XElement toXML()
        {
            var xml = new XElement("TjänsteMeddelande",
                new XElement("SvarsKod", AnswerCode),
                new XElement("Meddelande", Message),
                new XElement("Tjänstesansvarig", ServiceManager),
                new XElement("NamnOchVersion", NameVersion),
                new XElement("SvarsTid", AnswerTime),
                new XElement("Anropsansvarig", CallManager),
                new XElement("SökArgument", SentArgument)
                );

            return xml;
        }
    }
}