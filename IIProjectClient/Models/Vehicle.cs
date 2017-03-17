using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IIProjectClient.Models
{
    public class Vehicle
    {
        public string EPC { get; set; }
        public string EVN { get; set; }
        public string Owner { get; set; }
        public string Maintenance { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public Authorisation Auth { get; set; }

        public Vehicle()
        {
            this.Auth = new Authorisation();

        }
    }

}