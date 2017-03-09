using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IIProjectClient.Models
{
    public class Fordon
    {
        public string EPC { get; set; }
        public long EVN { get; set; }
        public static string Innehavare { get; set; }
        public string UHAnsvarig { get; set; }
        public string Kategori { get; set; }
        public string Underkategori { get; set; }
        public Godkännande Godkännande { get; set; }

    
    }
}