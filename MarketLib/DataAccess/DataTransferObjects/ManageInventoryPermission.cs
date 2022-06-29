using MarketLib.src.StoreNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class ManageInventoryPermissionDTO : StorePermissionDTO
    {
        public ManageInventoryPermissionDTO(StoreDTO store) : base(store) { }

        public override bool Equals(object obj)
        {
            var o = obj as ManageInventoryPermissionDTO;
            if (o != null)
                return this.store == o.store;
            return false;
        }

    }
}
