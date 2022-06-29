using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.StoreNS;

namespace MarketLib.src.StoreNS
{
    public class Auction
    {
        Product related_product;
        public double current_price;
        public string current_bidder;
        DateTime expiryDate;

        public Auction(Product related_product, double initial_price, int longevityInDays)
        {
            this.related_product = related_product;
            this.current_price = initial_price;
            this.expiryDate = DateTime.Now.AddDays(Convert.ToDouble(longevityInDays));

        }

        public void BidOnProductAsBuyer(string username, double price)
        {
            if (0 > price)
            {
                Console.WriteLine("error - the price bid by " + username + " is lower than zero");
                return;
            }
            if (current_price > price) {
                Console.WriteLine("error - the price bid by " + username + " is lower than current price " + current_price);
                return;
            }
            this.current_price = price;
            this.current_bidder = username;
        }

        public bool isAuctionExpired(DateTime currDate)
        {
            return currDate.CompareTo(expiryDate) > 0;
        }

        public string ToString()
        {
            return "\nAuction on product "+related_product.ProductName+", expires on "+expiryDate.ToString("dd-MM-yyyy")+": \n"+
                "\t Highest bidder: "+current_bidder+", price: "+current_price;
        }

    }
}
