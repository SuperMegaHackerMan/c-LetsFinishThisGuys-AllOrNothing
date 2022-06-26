using MarketLib.src.StoreNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.MarketSystemNS
{
    class ItemSearchManager
    {
        
        public ICollection<Product> filterProducts(ICollection<Store> stores,int store_rating,string name, string category, double startprice, double endprice, double rating )
        {
            ICollection<Product> filter = new List<Product>();
            ICollection<Store> stores2 = filter_stores_by_rating(stores, store_rating);
            foreach (Store store in stores2)
            {
                ICollection<Product> productsbyname= store.searchProductByName(name);
                ICollection<Product> productsbycategory = store.searchProductByCategory(category);
                List<Product> products = productsbyname.Intersect(productsbycategory).ToList();
                products =store.filterByPrice(products,startprice,endprice);
                products = store.filterByRating(products, rating);
                filter=  filter.Concat(products).ToList();
            }
            return filter;
        }

        public ICollection<Store> filter_stores_by_rating(ICollection<Store> stores, int store_rating)
        {         
            ICollection<Store> filter = new List<Store>();
            foreach (Store store in stores)
                if (store.getRating() >= store_rating)
                    filter.Add(store);
            return filter;
        }
    }
}
