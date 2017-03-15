using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IIProjectClient.Models
{
    public class Users
    {
        public List<string> userList = new List<string>();

        public Users()
        {
            this.userList.Add("Jonas");
            this.userList.Add("Albin");
            this.userList.Add("Magnus");
            this.userList.Add("Herminator");
        }
    }
}