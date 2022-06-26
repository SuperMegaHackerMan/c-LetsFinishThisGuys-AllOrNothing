using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.ExternalService.Supply
{
    public class Address
    {
        public string name;
        public string address;
        public string city;
        public string country;
        public string zip;

        public Address(string name, string address, string city, string country, string zip)
        {
            this.name = name;
            this.address = address;
            this.city = city;
            this.country = country;
            this.zip = zip;
        }

        public bool legalAddress()
        {
            try
            {
                int.Parse(address);
                return true;            
            }
            catch { return false; }
        }
    }
}
