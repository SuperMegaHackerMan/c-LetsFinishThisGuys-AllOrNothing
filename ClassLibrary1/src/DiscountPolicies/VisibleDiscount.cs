using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.DiscountPolicies
{
    class VisibleDiscount
    {
        int discountPercentOutOfHundred;
        int daysUntilDiscountExpiration;

        public VisibleDiscount(int discountPercentOutOfHundred, int daysUntilDiscountExpiration)
        {
            this.discountPercentOutOfHundred = discountPercentOutOfHundred;
            this.daysUntilDiscountExpiration = daysUntilDiscountExpiration;
        }
    }
}
