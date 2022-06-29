using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.DiscountPolicies
{
    class HiddenDiscount
    {

        int discountPercentOutOfHundred;
        int daysUntilDiscountExpiration;
        string dCode;

        public HiddenDiscount(int discountPercentageOutOfHundred, int daysUntilDiscountExpiration, string dCode)
        {
            this.discountPercentOutOfHundred = discountPercentOutOfHundred;
            this.daysUntilDiscountExpiration = daysUntilDiscountExpiration;
            this.dCode = dCode;
        }

    }
}
