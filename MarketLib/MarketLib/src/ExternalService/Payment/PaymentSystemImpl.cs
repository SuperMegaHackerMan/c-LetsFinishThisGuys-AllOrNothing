using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.ExternalService.Payment
{

    //same as supply, this is a proxy class.
    public class PaymentSystemImpl : PaymentSystem
    {
        private PaymentSystem paymentSys;
        public PaymentSystemImpl(PaymentSystem paymentSys)
        {
            this.paymentSys = paymentSys;
        }





        public int pay(CreditCard request)
        {
            if(request ==null || !request.isLegal())
            {
             //   throw new Exception("");
            }
            int ret = paymentSys.pay(request);
            if(ret==-1)
                throw new Exception("");
            return ret;

        }

        public int cancelPayment(int paymentid)
        {
            if (paymentid > 100000 | paymentid < 10000)
                throw new Exception("");
            int ret = paymentSys.cancelPayment(paymentid);
            if (ret ==-1)
            {
                throw new Exception("");
            }
            return ret;
        }

        public string handShake()
        {
            string ret = paymentSys.handShake();
            if (ret != "OK")
                throw new Exception();
            return ret;
        }
    }
}
