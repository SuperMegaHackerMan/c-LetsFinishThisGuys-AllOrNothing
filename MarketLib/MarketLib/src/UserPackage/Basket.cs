using MarketLib.src.StoreNS;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.UserP
{
    public class Basket
    {
        private int storeid;
        private ConcurrentDictionary<Product, int> products; // product and quantitiy.

        public ConcurrentDictionary<Product, int> Products { get => products; set => products = value; }

        public Basket(int storeid, ConcurrentDictionary<Product, int> products)
        {
            this.storeid = storeid;
            this.products = products;
        }

        public int getStore()
        {
            return storeid;
        }

        public int getQuantity(Product p)
        {
            return products[p];
        }

        public void setQuantity(Product p, int quantity)
        {
            products[p] = quantity;
        }

        /// <summary>
        /// adds to the current basket more to the quantity of the chosen product.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="quantity"></param>
        public void addProduct(Product p, int quantity)
        {
            var flag = products.ContainsKey(p);
            if (flag)
            {
                var current = products[p];
                int newquan = current + quantity;
                products[p] = newquan;
            }
            else
                products[p] = quantity;
        }

        public void removeProduct(Product p)
        {
            products.TryRemove(p,out _);
        }

    }
}
