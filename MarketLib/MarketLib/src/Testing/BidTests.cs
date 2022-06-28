using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using MarketLib.src.MarketSystemNS;


namespace ClassLibrary1.src.Testing
{
    class BidTests
    {

        public static void main(string[] args)
        {

            Store store = new Store();
            //StorePermission perm1 = new ManagerPermission(store);
            //StorePermission perm2 = new OwnerPermission(store);
            //Console.WriteLine(perm1.Equals(perm2));




            Console.WriteLine("starting test");
            ConcurrentDictionary<Product, int> products1 = new ConcurrentDictionary<Product, int>();
            ConcurrentDictionary<Product, int> products2 = new ConcurrentDictionary<Product, int>();
            Console.WriteLine("added product dictionaries");

            products1.TryAdd(new Product(1, "halav", 5.0, "dairy", 10.0), 3);
            products1.TryAdd(new Product(2, "betzim", 7.0, "dairy", 10.0), 4);
            products2.TryAdd(new Product(1, "halav", 5.0, "dairy", 10.0), 3);


            Basket b1 = new Basket(store.getId(), products1);
            Basket b2 = new Basket(store.getId(), products2);
            Console.WriteLine("created new baskets");

            ConcurrentDictionary<int, Basket> baskets1 = new ConcurrentDictionary<int, Basket>();
            ConcurrentDictionary<int, Basket> baskets2 = new ConcurrentDictionary<int, Basket>();
            Console.WriteLine("created concurrentDicts");


            baskets1.TryAdd(1, b1);
            baskets2.TryAdd(1, b2);
            Console.WriteLine("wrapped baskets in concurrentDicts");

            User pavel = new User(baskets1);
            User dan = new User(baskets2);
            Console.WriteLine("created users");

            MarketSystem ms = new MarketSystem();
            Console.WriteLine("created marketSystem");

            var conId1 = ms.connect();
            var conId2 = ms.connect();

            ms.register(conId1, "pavel", "pavelpass");
            ms.register(conId2, "dan", "danpass");
            Console.WriteLine("registered users");

            ms.login(conId1, "pavel", "pavelpass");
            ms.login(conId2, "dan", "danpass");
            Console.WriteLine("logged in users");
            int dans_store_id = ms.openNewStore("dan", "dans awesome store");
            ICollection<Store> stores = ms.StoresInfo();
            Console.WriteLine("created dans store");

            int first_product = ms.addProductToStore("dan", dans_store_id, "halav", "dairy", "expired", 2, 3);

            Console.WriteLine("added product to dans store");

            // # Print stores
            foreach (Store s in stores)
            {
                Console.WriteLine("\n# Store: " + s.getName() + ", Products: ");
                Console.WriteLine(s.toString());

            }

            int pavels_bidId = ms.bidOnItemAsBuyer("pavel", dans_store_id, "halav", 2, "dairy", "expired");
            Console.WriteLine("\n pavel bid on item");

            ms.acceptBidAsManager("dan", pavels_bidId);
            Console.WriteLine("\ndan has accepted pavels bid (and dan is the only manager, so the bid should be approved...)");

            Console.WriteLine("\nexpected = null, result: bids[bidId]=" + ms.getBid(pavels_bidId));

            // # Print dan's Basket
            Console.WriteLine("\n# Dan's basket:\n" + ms.getSubscriberByUserName("dan").getBasket(dans_store_id).toString());

            //ms.purchaseCart(conId2);
            //Console.WriteLine("purchased cart for connection " + conId2);

            // # Print dan's Basket
            Console.WriteLine("\n# Dan's basket:\n" + ms.getSubscriberByUserName("dan").getBasket(dans_store_id).toString());

            // # Print stores
            foreach (Store s in stores)
            {
                Console.WriteLine("\n# Store: " + s.getName() + ", Products: ");
                Console.WriteLine(s.toString());
            }

            Console.WriteLine("\nfinished printing stores & products");

            Console.WriteLine("finished test");

        }
    }
}
