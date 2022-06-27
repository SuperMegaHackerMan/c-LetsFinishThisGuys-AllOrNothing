using MarketLib.src.StoreNS;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketLib.src.UserP
{
    public class User
    {
        protected ConcurrentDictionary<int, Basket> baskets;

        public User()
        {
            this.baskets = new ConcurrentDictionary<int, Basket>(); // storeid , basket
        }

        public User(ConcurrentDictionary<int, Basket> baskets)
        {
            this.baskets = baskets;
        }

        /// <summary>
        /// will return the bascket by its store id.
        /// if we still havent initiated this basket we will make a new empty one.
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public Basket getBasket(int storeid)
        {

            if (baskets.ContainsKey(storeid) && baskets[storeid] != null)
            {
                return baskets[storeid];
            }
            baskets[storeid] = new Basket(storeid, new ConcurrentDictionary<Product, int>());
            return baskets[storeid];
        }

        public ConcurrentDictionary<int, Basket> getCart()
        {
            return baskets;
        }
    }
}
