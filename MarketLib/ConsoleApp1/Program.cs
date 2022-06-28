﻿using System.Collections.Concurrent;
using System;
using System.Collections;
using System.Collections.Generic;
using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using MarketLib.src.MarketSystemNS;

namespace ConsoleApp1
{
    class Program
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
            
            Store store = new Store();

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

            int pavels_bidId = ms.bidOnItemAsBuyer("pavel", dans_store_id,"halav", 2, "dairy", "expired");
            Console.WriteLine("\n pavel bid on item");

            ms.acceptBidAsManager("dan", pavels_bidId);
            Console.WriteLine("\ndan has accepted pavels bid (and dan is the only manager, so the bid should be approved...)");

            Console.WriteLine("\nexpected = null, result: bids[bidId]=" + ms.getBid(pavels_bidId));

            // # Print dan's Basket
            Console.WriteLine("\n# Dan's basket:\n" + ms.getSubscriberByUserName("dan").getBasket(dans_store_id).toString());

           



            Console.ReadLine();

        }
    }
}
