using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.ExternalService.Payment
{
    public class PaymentAdapter : PaymentSystem
    {
        private readonly string url;
        private static readonly HttpClient client = new HttpClient();

        public PaymentAdapter()
        {
            url = "https://cs-bgu-wsep.herokuapp.com/";
        }

        //POST REQUEST ASYNC 
        async public Task<int> sendRequest(Dictionary<string, string> request)
        {

            try
            {
                var content = new FormUrlEncodedContent(request);
                var response = await client.PostAsync(url, content);
                return int.Parse(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                throw new Exception("Error0");
            }
        }

        //POST REQUEST ASYNC 
        async public Task<string> sendHandRequest(Dictionary<string, string> request)
        {

            try
            {
                var content = new FormUrlEncodedContent(request);
                var response = await client.PostAsync(url, content);
                return (await response.Content.ReadAsStringAsync());

            }
            catch (Exception e)
            {
                throw new Exception("Error0");
            }
        }

        public string handShake()
        {
            try
            {
                Dictionary<string, string> requestBody = handShakeString();
                return sendHandRequest(requestBody).GetAwaiter().GetResult();/// TODO : check if this does not bring me any trouble.
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int pay(CreditCard request)
        {
            try
            {
                Dictionary<string, string> requestBody = paymentRequest(request);
                return sendRequest(requestBody).GetAwaiter().GetResult();/// TODO : check if this does not bring me any trouble.
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int cancelPayment(int request)
        {
            try
            {
                Dictionary<string, string> requestBody = cancel_paymentRequest(request);
                return sendRequest(requestBody).GetAwaiter().GetResult();/// TODO : check if this does not bring me any trouble.
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //prepare payment request.
        private Dictionary<string, string> paymentRequest(CreditCard credit)
        {
            Dictionary<string, string> req = new Dictionary<string, string>();

            req.Add("action_type", "pay");
            req.Add("card_number", credit.number);
            req.Add("month", credit.month);
            req.Add("year", credit.year);
            req.Add("holder", credit.holder);
            req.Add("ccv", credit.cvv);
            req.Add("id", credit.id);
            return req;
        }

        private Dictionary<string, string> cancel_paymentRequest(int transactionId)
        {
            Dictionary<string, string> req = new Dictionary<string, string>();

            req.Add("action_type", "pay");
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
