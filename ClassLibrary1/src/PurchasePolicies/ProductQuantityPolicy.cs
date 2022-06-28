using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.StoreNS;
using MarketLib.src.UserP;

namespace MarketLib.src.PurchasePolicies
{
    class ProductQuantityPolicy : BasePurchasePolicy
    {
        private readonly List<Product> products;
        private readonly int minQuantity;
        private readonly int maxQuantity;

        public ProductQuantityPolicy(List<Product> products, int minquantity, int max)
        {
            this.products = products;
            this.minQuantity = minquantity;
            this.maxQuantity = max;
        }
        public override bool checkValidPurchase(Basket basket)
        {
            foreach (KeyValuePair<Product, int> productAndQuantity in basket.Products)
            {
                Product p = productAndQuantity.Key;
                int q = productAndQuantity.Value;
                if (!products.Contains(p))
                {
                    return false;            
                }
                else if (!(minQuantity <= q) && !(maxQuantity >= q)) { return false; }
              
            }
            return true;

        }
    }
}
