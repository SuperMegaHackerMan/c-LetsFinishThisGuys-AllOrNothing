using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace MarketLib.src.ExternalService.Supply
{
    //we made this pattern so we could change between services in a dynamic fashion.
    public class DeliveryAdapter : SupplySystem
    {
        private readonly string url;
        private static readonly HttpClient client = new HttpClient();

        public DeliveryAdapter()
        {
           url = "https://cs-bgu-wsep.herokuapp.com/"; 
        }



        public override int cancel(int deliveryId)
        {
            try
            {
                Dictionary<string, string> requestBody = transactionToString(deliveryId);
                return sendRequest(requestBody).GetAwaiter().GetResult();/// TODO : check if this does not bring me any trouble.
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override int deliver(Address data)
        {
            try
            {
                Dictionary<string, string> requestBody = addressToString(data);
                return sendRequest(requestBody).GetAwaiter().GetResult();// TODO : check if this does not bring me any trouble.
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //POST REQUEST ASYNC 
        async public Task<int> sendRequest(Dictionary<string , string> request)
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

        public override string handshake()
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
    }
}
