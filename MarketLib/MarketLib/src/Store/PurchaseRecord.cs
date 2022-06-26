using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.StoreNS
{
    public class PurchaseRecord
    {
        private Dictionary<Product, int> products; //product ,  quantity.
        private string date;
        private double total_price;

        public PurchaseRecord(Dictionary<Product, int> products, string date, double total)
        {
            this.products = products;
            this.date = date;
            this.total_price = total;

        }

    }
}
