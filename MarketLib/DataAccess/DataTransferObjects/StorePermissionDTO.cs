using MarketLib.src.StoreNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class StorePermissionDTO
    {
        protected StoreDTO store;

        protected StorePermissionDTO(StoreDTO store)
        {
            this.store = store;
        }

        public StoreDTO getStore()
        {
            return store;
        }

        public string toString()
        {
            return GetType().ToString() + "{" +
                    "store=" + (store == null ? null : store.getName()) +
                    '}';
        }
    }
}
