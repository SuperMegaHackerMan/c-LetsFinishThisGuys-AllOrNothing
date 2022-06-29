using ClassLibrary1.src.DiscountPolicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.DiscountPolicies
{
    class ConditionalDiscount
    {

        int discountPercentOutOfHundred;
        int daysUntilDiscountExpiration;
        Condition condition;

        public ConditionalDiscount(Condition condition, int discountPercentOutOfHundred, int daysUntilDiscountExpiration)
        {
            this.condition = condition;
            this.discountPercentOutOfHundred = discountPercentOutOfHundred;
            this.daysUntilDiscountExpiration = daysUntilDiscountExpiration;
        }



    }
}
