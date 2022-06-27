using MarketLib.src.ExternalService.Payment;
using MarketLib.src.ExternalService.Supply;
using MarketLib.src.StoreNS;
using MarketLib.src.StorePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SupplySystemImpl s = new SupplySystemImpl(new DeliveryAdapter());
            PaymentSystemImpl soo = new PaymentSystemImpl(new PaymentAdapter());
            Console.WriteLine(soo.handShake());
            CreditCard cred = new CreditCard("dd", "dd", "dd", "dd", "dd", "dd");
            Console.WriteLine(soo.pay(cred));
            Console.WriteLine(s.handshake());
            Address add = new Address("poop", "poop", "poop", "poop", "poop");
            Console.WriteLine(s.deliver(add));

            //Purchasing TEST
            // 
            User pavel = new User();
            user dan = new user();
            // Basket.Items > Store.Items
            Basket b = new Basket(male, items);
            pavel.addBasket(b);
            try
            {
                pavel.purchaseCart();
                    // iterating over baskets that are per store
                    // if a purchase is bad revert all.
            }
            catch {// one basket is bad
                //quantity error -> to much in basket, store cant support
                    }
            alt_catch{ 
                //roll back purchases
            }
            PurchaseRecord new_purchase

            Store store = new Store();
            StorePermission perm1 = new ManagerPermission(store);
            StorePermission perm2 = new OwnerPermission(store);
            Console.WriteLine(perm1.Equals(perm2));
            Console.ReadLine();

        }
    }
}
