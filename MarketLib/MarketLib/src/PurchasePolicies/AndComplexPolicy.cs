using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.PurchasePolicies
{
    class AndComplexPolicy: ComplexPurchasePolicy
    {
        public AndComplexPolicy(List<PurchasePolicy> policies) : base(policies)
        {

        }
        public override bool checkValidPurchase(Basket basket)
        {
            foreach (PurchasePolicy p in this.policies)
            {
                if (!p.checkValidPurchase(basket))
                    return false;
            }
            return true;
        }
    }
}
