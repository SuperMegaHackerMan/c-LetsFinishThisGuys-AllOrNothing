using java.util;
using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.StoreNS
{
    public class Inventory
    {
        private Dictionary<Product, int> products; //<k: Product, v: int>
        private int idpatcher;

        public Dictionary<Product, int> Products { get => products; }

        public Inventory()
        {
            products = new Dictionary<Product, int>();
            idpatcher = 1;
        }

        public string toString()
        {
            string s = "";
            foreach (var p in products)
            {
                s = s + p.Key.ToString() + ", amount: " + p.Value.ToString() + '\n';
            }
            return s;
        }

        /// <summary>
        ///  adds a new item into the store inventory 
        ///  needs to be sychronized so it will stay stable with multiple users
        /// </summary>
        /// <returns></returns>
        public int addProduct(string name, double price, string category, int amount)
        {
            if (name == null || name.Equals(""))
                throw new Exception("item name is illegal");
            if (price <= 0)
                throw new Exception("item price is iilegal");
            if (amount <= 0)
                throw new Exception("ilegal item amount");

            lock (products)
            {
                foreach (Product p in products.Keys)
                    if (searchItemByName(name).Count != 0 && searchItemByCategory(category).Count !=0)
                        throw new Exception("item already exists");
                products.Add(new Product(this.idpatcher, name, price, category, 0), amount);
                return idpatcher++;

            }
        }

        //searches for the item with the matching name
        public List<Product> searchItemByName(string name)
        {
            List<Product> foundProducts = new List<Product>() ; // Products
            foreach (Product p in products.Keys)
            {
                if (p.ProductName.ToUpper() == name.ToUpper())
                    foundProducts.Add(p);
            }
            return foundProducts;
        }

        //find the matching items by category
        public List<Product> searchItemByCategory(string category)
        {
            List<Product> foundProducts = new List<Product>();
            foreach (Product p in products.Keys)
            {
                if (p.Category.ToUpper() == category.ToUpper())
                    foundProducts.Add(p);
            }
            return foundProducts;
        }


        public Product searchItem(int itemId)
        {
            foreach (Product p in products.Keys)
                if (p.ProductId == itemId)
                    return p;
            throw new Exception("item not found");
        }

        //gives you the list of products by the pricemark
        public List<Product> filterByPrice(double startPrice, double endprice)
        {
            List<Product> foundProducts = new List<Product>();
            foreach (Product p in products.Keys)
            {
                if (p.Price >= startPrice && p.Price <= endprice)
                    foundProducts.Add(p);
            }
            return foundProducts;
        }


        public List<Product> filterByRating(double rating)
        {
            List<Product> foundProducts = new List<Product>();

            foreach (Product p in products.Keys)
            {
                if (p.Rating >= rating)
                    foundProducts.Add(p);
            }
            return foundProducts;
        }



        public Product getItem(string name, string category, String subCategory)
        {
            foreach (Product p in products.Keys)
                if (p.ProductName.Equals(name) && p.Category.Equals(category))
                    return p;
            throw new Exception("ItemNotFoundException : item not found");
        }


        public void changeQuantity(int itemId, int amount)
        {
            if (amount < 0)
                throw new Exception("item amount should be 0 or more than that");
            lock (this.products)
            {
                var am = searchItem(itemId);
                products[am]= amount;
            }

        }

        //checks the stock for a certin item 
        public bool checkAmount(int itemId, int amount)
        {
            if (amount > getProductAmount(itemId))
                throw new Exception("there is not enough from the item");
            if (amount < 0)
                throw new Exception("amount can't be a negative number");
            return true;
        }

        //removes an item from the inventory.
        public Product removeItem(int itemID)
        {
            Product item = searchItem(itemID);
            products.Remove(item);
            return item;
        }

        public void changeItemDetails(int itemID, int newQuantity, double newPrice)
        {
            //synchronized (this.items)
            //{
            foreach (Product p in products.Keys)
            {
                if (p.ProductId == itemID)
                {

                    if (newQuantity != null)
                        changeQuantity(itemID, newQuantity);

                    if (newPrice != null)
                        p.Price = newPrice;

                    return;
                }
            }
            throw new Exception("no item in inventory matching item id");
            // }
        }

        public int getProductAmount(int productid)
        {
            Product p = searchItem(productid);
            return products[p];
        }


        static object locker = new object();

        /// <summary>
        /// supposed to calculate our basket and also ujpdate the bascket amount.
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public double calculateBasket(Basket basket)
        {
            lock (locker) // we are modifying therfore we require a blovking method.
            {
                double total = 0; //TODO: add the discounts.
                bool flag = true;
                foreach (KeyValuePair<Product, int> product in basket.Products)
                {
                    if (flag)
                        flag = checkAmount(product.Key.ProductId, product.Value);
                }
                if (!flag)
                    throw new Exception("store dosent have this amount");
                else
                    foreach (KeyValuePair<Product, int> product in basket.Products)
                    {
                        int newAmount = products[product.Key] - product.Value;//inventory - bascket.
                        total += product.Key.Price * product.Value;
                        changeQuantity(product.Key.ProductId, newAmount);
                    }
                return total;
            }
        }
    }
}
