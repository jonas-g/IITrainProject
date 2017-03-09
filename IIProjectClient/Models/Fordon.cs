﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IIProjectClient.Models
{
    public class Fordon
    {
        public string EPC { get; set; }
        public long EVN { get; set; }
        public string Innehavare { get; set; }
        public string UHAnsvarig { get; set; }
        public string Kategori { get; set; }
        public string Underkategori { get; set; }
        public Godkännande Godkännande { get; set; }

        public bool Godkänd()
        {
            if (Godkännande.Slut < DagensDatum)  //Chansning!
            {
                return false;
            } else {
                return true;
            }
            
        }
    }
}