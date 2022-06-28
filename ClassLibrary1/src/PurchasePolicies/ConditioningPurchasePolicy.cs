using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.UserP;

namespace MarketLib.src.PurchasePolicies
{
    class ConditioningPurchasePolicy : ComplexPurchasePolicy
    {
        private List<PurchasePolicy> then;
        public ConditioningPurchasePolicy(List<PurchasePolicy> policies, List<PurchasePolicy> then) :base(policies)
        {
            this.then = then;
        }
        public override bool checkValidPurchase(Basket basket)
        {
            foreach (PurchasePolicy p in this.policies)//check if the statment holds
            {
                if (!p.checkValidPurchase(basket))
                    return false;
            }
            foreach (PurchasePolicy p in this.then)//check if 'then' statement holds
            {
                if (!p.checkValidPurchase(basket))
                    return false;
            }
            return true;

        }

    }
}
