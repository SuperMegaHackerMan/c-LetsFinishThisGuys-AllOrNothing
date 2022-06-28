using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.UserP;

namespace MarketLib.src.PurchasePolicies
{
    abstract class ComplexPurchasePolicy: PurchasePolicy
    {

        public ComplexPurchasePolicy(List<PurchasePolicy> policies)
        {
            this.policies = policies;
        }
       internal  List<PurchasePolicy> policies = new  List<PurchasePolicy>();

        abstract public bool checkValidPurchase(Basket basket);

        public void add(BasePurchasePolicy policy)
        {
            policies.Add(policy);
        }

        public void remove(BasePurchasePolicy policy)
        {
            policies.Remove(policy);
        }

        public List<PurchasePolicy> GetPurchasePolicies()
        {
            return policies;
        }
    }
}
