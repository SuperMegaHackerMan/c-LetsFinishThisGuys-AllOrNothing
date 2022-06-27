using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.StorePermission
{
    public class AppointerPermission :StorePermission
    {
        private Subscriber target;

        public AppointerPermission(Subscriber target, Store store) : base(store)
        {
            this.target = target;
        }

        public Subscriber getTarget()
        {
            return target;
        }


        public override bool Equals(object obj)
        {
            var o = obj as AppointerPermission;
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
