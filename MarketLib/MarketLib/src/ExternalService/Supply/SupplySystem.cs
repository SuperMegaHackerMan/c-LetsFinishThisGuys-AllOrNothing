using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.ExternalService.Supply
{
    public abstract class SupplySystem
    {

        //This action type is used for check the availability of the external systems. 
     public abstract string handshake();

    //This action type is used for dispatching a delivery to a costumer.
    public abstract int deliver(Address data) ;

    // This action type is used for cancelling a supply transaction.
    public abstract int cancel(int deliveryId);

        //prepares the data for the supply request
       public Dictionary<string, string> addressToString(Address address)
        {
            Dictionary<string, string> req = new Dictionary<string, string>();
            req.Add("action_type", "supply");
            req.Add("name", address.name);
            req.Add("address", address.address);
            req.Add("city", address.city);
            req.Add("country", address.country);
            req.Add("zip", address.zip);
            return req;
        }

    //prepares the data for the cancel_supply request 
    public Dictionary<string, string> transactionToString(int transactionId)
        {
            Dictionary<string, string> req = new Dictionary<string, string>();
            req.Add("action_type", "cancel_pay");
            req.Add("transaction_id", transactionId.ToString());
            return req;
        }
        public Dictionary<string, string> handShakeString()
        {
            Dictionary<string, string> req = new Dictionary<string, string>();
            req.Add("action_type", "handshake");
            return req;
        }

        }
}
