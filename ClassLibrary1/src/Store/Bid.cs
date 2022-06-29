using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketLib.src.StoreNS
{
    public class Bid
    {
        public Product related_product;
        public Store related_store;
        public double currrent_price;
        public Boolean isAccepted;
        public string bidder_username;
        public Boolean isOpen;
        public int numberOfAccepts;

        public Bid(Store related_store, Product related_product, double price, string bidder_username)
        {
            // bid is firstly created by buyer
            this.related_product = related_product;
            this.currrent_price = price;
            this.bidder_username = bidder_username;
            this.related_store = related_store;
            this.isOpen = true;
        }

        public Bid() { }

        // 1. manager functionality:

        public int acceptBidAsManager() // not neccessarily a manager, could be an owner or someone with proper permissions.
        {
            if (!isOpen)
            {
                Console.WriteLine("this bid is closed");
                return 0;
            }
            if (isAccepted)
            {
                Console.WriteLine("this bid was already accepted");
                return 0;
            }
            this.numberOfAccepts = this.numberOfAccepts + 1;
            if (this.numberOfAccepts == this.related_store.managerCounter)
            {
                // means every manager approved this bid
                this.isAccepted = true;
                this.isOpen = false;

                int prodId = this.related_product.ProductId;
                double price = this.related_product.Price;
                int prevAmount = related_store.getInventory().getProductAmount(prodId);

                this.related_store.changeItem(prodId, prevAmount - 1, price);
            }
            return 1;
        }

        public bool checkIfManager(string maybeManagerId)
        {
            return this.related_store.checkIfManager(maybeManagerId);
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

        public Store getRelatedStore()
        {
            return related_store;
        }

        public string getRelatedBidderUsername()
        {
            return bidder_username;
        }

    }
}