using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class AppointerPermissionDTO : StorePermissionDTO
    {
        private SubscriberDTO target;

        public AppointerPermission(SubscriberDTO target, StoreDTO store) : base(store)
        {
            this.target = target;
        }

        public Subscriber getTarget()
        {
            return target;
        }


        public override bool Equals(object obj)
        {
            var o = obj as AppointerPermissionDTO;
            if (o != null)
                return this.store == o.store && this.target == o.target;
            return false;
                
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }



    }
}
