using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.PurchasePolicies;
using MarketLib.src.UserP;

namespace MarketLib.src.DiscountPolicies
{
    abstract class ComplexDiscountPolicy : DiscountPolicy
    {

        internal PurchasePolicy policies = null; //i can infer that this can be if we dont want any policy.

        public double calculateBasketTotal(Basket basket)
        {
            throw new NotImplementedException();
        }


        // abstract public double calculateBasketTotal(Basket basket);

    }
}
