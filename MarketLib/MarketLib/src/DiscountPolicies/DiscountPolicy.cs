using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.DiscountPolicies
{
    interface DiscountPolicy
    {
        double calculateBasketTotal(Basket basket);

    }
}
