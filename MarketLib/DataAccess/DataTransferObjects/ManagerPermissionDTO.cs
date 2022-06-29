using MarketLib.src.StoreNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class ManagerPermissionDTO:StorePermission
    {
        public ManagerPermissionDTO(StoreDTO store) : base(store)
        {
        }

        public override bool Equals(object obj)
        {
            var o = obj as ManagerPermissionDTO;
            if (o != null)
                return this.store == o.store;
            return false;
        }

    }
}
