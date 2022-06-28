using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.StoreNS;
using MarketLib.src.UserP;

namespace MarketLib.src.DiscountPolicies
{
    abstract class BaseDiscountPolicy : DiscountPolicy
    {
        internal int discount;
        internal List<Product> products; //products which have the discount.

        public BaseDiscountPolicy(int discount, List<Product> products)
        {
            this.discount = discount;
            this.products = products;
        }
        public double calculateBasketTotal(Basket basket)
        {
            double total = 0;
            foreach(KeyValuePair<Product, int> productAndQuantity in basket.Products)
            {
                Product p = productAndQuantity.Key;
                int q = productAndQuantity.Value;
                if (products.Contains(p))
                {
                    total += (p.Price * q) - (p.Price * q) * (discount / 100);
                }
                else
                    total += (p.Price * q);
            }
            return total;
        }

    }
}
