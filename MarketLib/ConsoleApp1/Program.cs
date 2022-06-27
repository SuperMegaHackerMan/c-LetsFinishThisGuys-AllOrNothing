﻿using System.Collections.Concurrent;
using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using MarketLib.src.MarketSystemNS;
using System;
using System.Collections;
using System.Collections.Generic;

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
            //StorePermission perm1 = new ManagerPermission(store);
            //StorePermission perm2 = new OwnerPermission(store);
            //Console.WriteLine(perm1.Equals(perm2));

            //testing purchases.
            // 1. dan store owner pavel buys 1 apple // GOOD CASE
            // 2. dan store owner pavel buys 2 apple // BAD CASE
            ConcurrentDictionary<Product, int> products1 = new ConcurrentDictionary<Product, int>();
            ConcurrentDictionary<Product, int> products2 = new ConcurrentDictionary<Product, int>();
            // 1 halav in store2 (dan)
            products2.TryAdd(new Product(1,"halav",5.0,"dairy",10.0), 1);


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

            ms.register(conId1,"pavel","pavelpass");
            ms.register(conId2,"dan","danpass");
            Console.WriteLine("registered users");

            ms.login(conId1,"pavel","pavelpass");
            ms.login(conId2,"dan","danpass");
            Console.WriteLine("logged in users");
            int dans_store_id = ms.openNewStore("dan", "dans awesome store");
            ICollection<Store> stores = ms.StoresInfo();
            Console.WriteLine("created dans store");

            ms.addProductToStore("dan", dans_store_id, "halav", "dairy", "expired", 1, 3);
            Console.WriteLine("added product to dans store");

            foreach (Store s in stores) {
                Console.WriteLine("\nStore: "+s.getName()+", Products: ");
                foreach (Product p in s.getInventory().Products.Keys) {
                    Console.WriteLine("Product name: "+ p.ProductName+", Product price: "+p.Price);
                }
            }
            Console.WriteLine("\nfinished printing stores & products");

            Console.WriteLine("finished test");

            Console.ReadLine();

        }
    }
}
