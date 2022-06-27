using java.util;
using MarketLib.src.DiscountPolicies;
using MarketLib.src.PurchasePolicies;
using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.StoreNS
{
    public class Store
    {
        private PurchasePolicy purchase_policy;
        private DiscountPolicy discount_policy;
        private int id;
        private string name;
        private string description;
        private double rating;
        private string founderUserName;
        private bool isActive;
        private Inventory inventory = new Inventory();
        private ArrayList purchases = new ArrayList(); // arrayList<string> : purcheses
                                                       //private Observable observable;

        public Store() { }

        /**
         * This method opens a new store and create its inventory
         *
         * @param name        - the name of the new store
         * @param description - the price of the new store
         *                    //  * @param founder - the fonder of the new store
         * @throws WrongNameException
         */
        public Store(int id, string name, string UserName, string description="default shop desc")
        {//, Observable observable) {
            if (name == null || name.Equals("") || name.Trim().Equals(""))
                throw new Exception("WrongNameException: store name is null or contains only white spaces");
            if (name.ToCharArray()[0] >= '0' && name.ToCharArray()[0] <= '9')
                throw new Exception("WrongNameException : store name cannot start with a number");
            if (description == null || description.Trim().Equals(""))
                throw new Exception("WrongNameException :  store description is null or contains only white spaces");
            // if (description.ToCharArray()[0] >= '0' && description.ToCharArray()[0] <= '9')
            //   throw new Exception( "WrongNameException: store description cannot start with a number");
            this.id = id;
            this.name = name;
            this.description = description;
            this.rating = 0;
            this.founderUserName = UserName;
            //this.observable = observable;
        }

        public int getId()
        {
            return id;
        }

        public String getName()
        {
            return name;
        }

        public void setDescription(string newDesc) {
            this.description = newDesc;
        }                                                  

        public String getDescription()
        {
            return description;
        }

        public double getRating()
        {
            return rating;
        }

        public void setRating(double rating)
        {
            if (rating < 0)
                throw new Exception(" WrongRatingException: rating must be a positive number");
            this.rating = rating;
        }

        public Inventory getInventory()
        {
            return inventory;
        }




        public int addItem(string name, double price, string category, string subCategory, int amount)
        {
            return this.inventory.addProduct(name, price, category, amount);
        }





        public Product getItem(string name, string category, string subCategory)
        {
            return this.inventory.getItem(name, category, subCategory);
        }


        public Product searchItemById(int productId)
        {
            return this.inventory.searchItem(productId);
        }

        public List<Product> searchProductByName(string name)
        {
            return inventory.searchItemByName(name);
        }

        public List<Product> searchProductByCategory(string category)
        {
            return inventory.searchItemByCategory(category);
        }

        public List<Product> filterByPrice(List<Product> products, double startPrice, double endPrice)
        {
            if (startPrice <= 0 || endPrice <= 0 || products == null)
                throw new Exception();
                return products.Intersect(inventory.filterByPrice(startPrice, endPrice)).ToList();

        }

        public List<Product> filterByRating(List<Product> products, double rating)
        {
            if (rating < 0 || products == null)
                throw new Exception();
            return products.Intersect(inventory.filterByRating(rating)).ToList();
        }


        public bool checkAmount(int productId, int amount)
        {
            return this.inventory.checkAmount(productId, amount);
        }


        public Product removeItem(int productId)
        {
            return this.inventory.removeItem(productId);
        }

        public string toString()
        {
            return inventory.ToString();
        }

        public void changeItem(int itemID, int newQuantity, double newPrice)
        {
            this.inventory.changeItemDetails(itemID, newQuantity, newPrice);
        }

        public bool ifActive()
        {
            return isActive;
        }

        public void setNotActive()
        {
            if (this.isActive == false)
                return;
            this.isActive = false;
            // observable.notifyStoreStatus(isActive);
        }

        public ArrayList getPurchaseHistory() { return purchases; }


        public void setActive()
        {
            if (this.isActive == true)
                return;
            this.isActive = true;
            // observable.notifyStoreStatus(isActive);
        }
        public void addPurchase(string purchaseDetails)
        {
            this.purchases.Add(purchaseDetails);
        }

        public double calculateBasket(Basket basket)
        {
            return inventory.calculateBasket(basket);
        }


        public void unlockItems(Set products)
        {
            foreach (Product p in products.toArray())
                p.unlockstore();
        }


    }
}
