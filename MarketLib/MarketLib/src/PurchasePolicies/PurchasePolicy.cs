using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.PurchasePolicies
{
    public interface PurchasePolicy
    {

        bool checkValidPurchase(Basket basket);

        List<PurchasePolicy> GetPurchasePolicies();
    }
}
