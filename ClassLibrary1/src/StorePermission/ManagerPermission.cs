using MarketLib.src.StoreNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.StorePermission
{
    public class ManagerPermission:StorePermission
    {
        public ManagerPermission(Store store) : base(store)
        {
        }

        public override bool Equals(object obj)
        {
            var o = obj as ManagerPermission;
            if (o != null)
                return this.store == o.store;
            return false;
        }

    }
}
