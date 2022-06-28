using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.StoreNS;
using MarketLib.src.UserP;

namespace MarketLib.src.PurchasePolicies
{
    /// <summary>
    /// this policy allows payment only if the value reached a certin goal.
    /// </summary>
    class BasketValuePolicy : BasePurchasePolicy
    {
        private readonly double basketValue;
        public BasketValuePolicy(double basketValue)
        {
            if (basketValue < 0)
                throw new Exception();
            this.basketValue = basketValue;
        }
        public override bool checkValidPurchase(Basket basket)
        {
            double current = 0;
            foreach ( KeyValuePair<Product, int> productAndQuantity in basket.Products)
            {
                Product pro = productAndQuantity.Key;
                int quan = productAndQuantity.Value;
                current += pro.Price * quan;
            }
            return !(current <= this.basketValue);
        }
    }
}
