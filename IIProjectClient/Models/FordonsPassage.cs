using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace IIProjectClient.Models
{
    public class FordonsPassage
    {
        public Fordon Fordonet { get; set; }
        public Plats Platsen { get; set; }
        public DateTime EventTid { get; set; }

        public FordonsPassage()
        {
            XElement XXXXXXX = new XElement("", "");
        }
    }
}