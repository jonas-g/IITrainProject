﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}