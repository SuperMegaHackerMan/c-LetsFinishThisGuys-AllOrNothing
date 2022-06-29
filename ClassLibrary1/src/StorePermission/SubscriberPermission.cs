using MarketLib.src.StoreNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.StorePermission
{
    public class SubscriberPermission
    {
        public SubscriberPermission()
        {

        }

        public override bool Equals(object obj)
        {
            var o = obj as SubscriberPermission;
            if (o != null)
                return true;
            return false;
        }
    }
}
