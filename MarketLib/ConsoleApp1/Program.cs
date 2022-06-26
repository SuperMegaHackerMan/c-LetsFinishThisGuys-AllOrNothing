using System.Collections.Concurrent;
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
            //Store store = new Store();
            //StorePermission perm1 = new ManagerPermission(store);
            //StorePermission perm2 = new OwnerPermission(store);
            //Console.WriteLine(perm1.Equals(perm2));
            Console.WriteLine("starting test");
            ConcurrentDictionary<Product, int> products1 = new ConcurrentDictionary<Product, int>();
            ConcurrentDictionary<Product, int> products2 = new ConcurrentDictionary<Product, int>();
            Console.WriteLine("added product dictionaries");

            products1.AddOrUpdate(new Product(1,"halav",5.0,"dairy",10.0), 3);
            products1.AddOrUpdate(new Product(2,"betzim",7.0,"dairy",10.0), 4);
            products2.AddOrUpdate(new Product(1,"halav",5.0,"dairy",10.0), 3);


            Basket b1 = new Basket(store.getId(), products1);
            Basket b2 = new Basket(store.getId(), products2);
            Console.WriteLine("created new baskets");

            ConcurrentDictionary<int, Basket> baskets1 = new ConcurrentDictionary<int, Basket>();
            ConcurrentDictionary<int, Basket> baskets2 = new ConcurrentDictionary<int, Basket>();
            Console.WriteLine("created concurrentDicts");


            baskets1.AddOrUpdate(1, b1);
            baskets2.AddOrUpdate(1, b2);
            Console.WriteLine("wrapped baskets in concurrentDicts");

            User pavel = new User(baskets1);
            User dan = new User(baskets2);
            Console.WriteLine("created users");

            MarketSystem ms = new MarketSystem();
            Console.WriteLine("created marketSystem");
            
            ms.register("connectionId1","pavel","pavelpass");
            ms.register("connectionId2","dan","danpass");
            Console.WriteLine("registered users");

            ms.login("connectionId1","pavel","pavelpass");
            ms.login("connectionId2","dan","danpass");
            Console.WriteLine("logged in users");

            int dans_store_id = ms.openNewStore("dan", "dans awesome store");
            Console.WriteLine("created dans store");

            ms.addProductToStore("dan", dans_store_id, "halav", "dairy", "expired", 420, 3);
            Console.WriteLine("added product to dans store");

            ICollection<Store> stores = ms.StoresInfo();
            foreach (Store s in stores) {
                Console.WriteLine("\nStore: "+s.getName+", Products: ");
                foreach (Product p in s.getInventory().Products) {
                    Console.WriteLine("Product name: "+ p.ProductName+", Product price: "+p.Price);
                }
            }
            Console.WriteLine("\nfinished printing stores & products");

            Console.WriteLine("finished test");

            Console.ReadLine();

        }
    }
}
