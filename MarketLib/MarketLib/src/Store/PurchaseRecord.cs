using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.StoreNS
{
    public class PurchaseRecord
    {
        private ConcurrentDictionary<Product, int> products; //product ,  quantity.
        private string date;
        private double total_price;

        public PurchaseRecord(ConcurrentDictionary<Product, int> products, string date, double total)
        {
            this.products = products;
            this.date = date;
            this.total_price = total;

        }

    }
}
