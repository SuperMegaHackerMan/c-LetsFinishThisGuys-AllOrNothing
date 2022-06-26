using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.UserP;

namespace MarketLib.src.PurchasePolicies
{
    class OrComplexPolicy : ComplexPurchasePolicy
    {

        public OrComplexPolicy(List<PurchasePolicy> policies):base(policies)
        {
            
        }
        public override bool checkValidPurchase(Basket basket)
        {
            foreach (PurchasePolicy p in  this.policies)
            {
                if (p.checkValidPurchase(basket))
                    return true;
            }
            return false;
        }
    }
}
