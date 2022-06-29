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
    class BidOnNonBidableProductTest
    {
        

        static void Main(string[] args)
        {
            //SupplySystemImpl s = new SupplySystemImpl(new DeliveryAdapter());
            //PaymentSystemImpl soo = new PaymentSystemImpl(new PaymentAdapter());
            //Console.WriteLine(soo.handShake());
            //CreditCard cred = new CreditCard("dd", "dd", "dd", "dd", "dd", "dd");
            //Console.WriteLine(soo.pay(cred));
            //Console.WriteLine(s.handshake());
            //Address add = new Address("poop", "poop", "poop", "poop", "poop");
            //Console.WriteLine(s.deliver(add));
            string testName = "BidOnNonBidableProductTest";
            Store store = new Store();

            Console.WriteLine("\n# # # Starting Test: " + testName);
            ConcurrentDictionary<Product, int> products1 = new ConcurrentDictionary<Product, int>();
            ConcurrentDictionary<Product, int> products2 = new ConcurrentDictionary<Product, int>();
            Console.WriteLine("\n# Created Product concurrentDicts");

            products1.TryAdd(new Product(1, "halav", 5.0, "dairy", 10.0), 3);
            products1.TryAdd(new Product(2, "betzim", 7.0, "dairy", 10.0), 4);
            products2.TryAdd(new Product(1, "halav", 5.0, "dairy", 10.0), 3);
            Console.WriteLine("\n# Added Products to the concurrentDicts");


            Basket b1 = new Basket(store.getId(), products1);
            Basket b2 = new Basket(store.getId(), products2);
            Console.WriteLine("\n# Created new baskets");

            ConcurrentDictionary<int, Basket> baskets1 = new ConcurrentDictionary<int, Basket>();
            ConcurrentDictionary<int, Basket> baskets2 = new ConcurrentDictionary<int, Basket>();
            Console.WriteLine("\n# Created Basket concurrentDicts");


            baskets1.TryAdd(1, b1);
            baskets2.TryAdd(1, b2);
            Console.WriteLine("\n# Added Baskets to the concurrentDicts");

            User pavel = new User(baskets1);
            User dan = new User(baskets2);
            Console.WriteLine("\n# Created users: pavel, dan");

            MarketSystem ms = new MarketSystem();
            Console.WriteLine("\n# Created marketSystem");

            var conId1 = ms.connect();
            var conId2 = ms.connect();

            ms.register(conId1, "pavel", "pavelpass");
            ms.register(conId2, "dan", "danpass");
            Console.WriteLine("\n# Registered users");

            ms.login(conId1, "pavel", "pavelpass");
            ms.login(conId2, "dan", "danpass");
            Console.WriteLine("\n# Logged in users");
            int dans_store_id = ms.openNewStore("dan", "dans awesome store");
            ICollection<Store> stores = ms.StoresInfo();
            Console.WriteLine("\n# Created store: '" + ms.getStore(dans_store_id).getName() + "'");

            int first_product = ms.addProductToStore("dan", dans_store_id, "halav", "dairy", "expired", 2, 3);

            Console.WriteLine("\n# Added product to dans store");

            // # Print stores
            foreach (Store s in stores)
            {
                Console.WriteLine("\n# Store: " + s.getName() + ", Products: ");
                Console.WriteLine(s.toString());

            }

            int pavels_bidId = ms.bidOnItemAsBuyer("pavel", dans_store_id, "halav", 2, "dairy", "expired");
            Console.WriteLine("\n pavel bid on item, expected to fail (item is of type Regular)");

            Console.WriteLine("checking if pavels bid is in bids (expected empty/null): bids[id]=" + ms.getBid(pavels_bidId));

            Console.WriteLine("\n# # # Finished test");

            Console.ReadLine();

        }
    }
}
