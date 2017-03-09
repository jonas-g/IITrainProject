using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

namespace IIProjectService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITrainService
    {

        // TODO: Add your service operations here

        [OperationContract]
        XElement GetPassageInfo(DateTime fromDate, DateTime toDate, string epcLocation);

        [OperationContract]
        void SaveToFile(XElement value);

        [OperationContract]
        IEnumerable<XElement> GetEvents(DateTime fromDate, DateTime toDate, string epcLocation);

        [OperationContract]
        XElement GetVehicle(string epc);

        [OperationContract]
        XElement GetLocation(string epc);

        [OperationContract]
        XElement GetAllLocations ();



    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
