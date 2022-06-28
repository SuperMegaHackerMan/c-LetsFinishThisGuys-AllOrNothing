using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.UserP;

namespace MarketLib.src.PurchasePolicies
{
    public abstract class BasePurchasePolicy : PurchasePolicy
    {
        abstract public bool checkValidPurchase(Basket basket);


        public List<PurchasePolicy> GetPurchasePolicies() { return null; }

    }
}
