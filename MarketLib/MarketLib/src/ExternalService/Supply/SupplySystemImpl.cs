using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.ExternalService.Supply
{
    //this class goes according to the proxy design pattern.
    public class SupplySystemImpl : SupplySystem
    {
        private readonly string goodMessage = "OK"; // the messsage sent when there is a succeful  handshake
        private SupplySystem realSupply;

        public SupplySystemImpl(SupplySystem s)
        {
            realSupply = s;
        }
        public override int cancel(int deliveryId)
        {
            if (deliveryId > 100000 | deliveryId < 10000)
            {
                throw new Exception("");
            }
            int ret = realSupply.cancel(deliveryId);
            if (ret  == -1)
            {
                throw new Exception("");
            }
            return ret;
        }

        public override int deliver(Address data)
        {
            if (data == null)
            {
                throw new Exception("Address not supplied");
            }
            if (!data.legalAddress())
            {
              //  throw new Exception("Address details are illegal");
            }
            int ret = realSupply.deliver(data);
            if (ret == -1)
            {
                throw new Exception("");
            }
            return ret;
        }

        public override string handshake()
        {
            string ret = realSupply.handshake();
            if(ret != "OK")
                throw new Exception("");
            return ret;
        }


       
    }
}
