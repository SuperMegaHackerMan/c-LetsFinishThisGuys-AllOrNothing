using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.StoreNS;


namespace MarketLib.src.StoreNS
{
    public class Bid
    {
        public Product related_product;
        public Store related_store;
        public double currrent_price;
        public Boolean isAccepted;
        public string related_user_id;
        public Boolean isOpen;

        public Bid(Store store, Product related_product, double price, string related_user_id)
        {
            // created by buyer
            this.related_product = related_product;
            this.currrent_price = price;
            this.related_user_id = related_user_id;
            this.isOpen = true;
        }

        // 1. manager functionality:

        public void acceptBidAsManager() // not neccessarily a manager, could be an owner or someone with proper permissions.
        {
            this.isAccepted = true;
            this.isOpen = false;
        }

        public void declineBidAsManager() // not neccessarily a manager, could be an owner or someone with proper permissions.
        {
            this.isAccepted = false;
            this.isOpen = false;
        }

        public void giveCounterOfferAsManager(double counter_offer)
        {
            if (!isOpen)
            {
                Console.WriteLine("bid is already closed, could not accept counter offer");
                return;
            }
            if (counter_offer < currrent_price)
            {
                Console.WriteLine("counter offer is lower than current price on table, could not accept counter offer");
                return;
            }
            this.currrent_price = counter_offer;
        }

        // 2. buyer functionality:

        public void acceptCounterOfferAsBuyer()
        {
            this.isAccepted = true;
            this.isOpen = false;
        }
        public void declineCounterOfferAsBuyer()
        {
            this.isAccepted = false;
            this.isOpen = false;
        }

    }
}