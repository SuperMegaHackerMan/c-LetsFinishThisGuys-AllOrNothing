using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.ExternalService.Payment
{
    public class CreditCard
    {
        public string number;
        public string month;
        public string year;
        public string cvv;
        public string holder;
        public string id;

        public CreditCard(string number, string month, string year, string cvv, string holder, string id)
        {
            this.number = number;
            this.month = month;
            this.year = year;
            this.cvv = cvv;
            this.holder = holder;
            this.id = id;
        }

        public bool isLegal()
        {
            try
            {
                int month = int.Parse(this.month);
                int year = int.Parse(this.year);
                int cvv = int.Parse(this.cvv);
                long number = long.Parse(this.number);
                return (month > 0 & month < 13 & year > 2000 & year < 2030 & !string.IsNullOrEmpty(id) &
                       !string.IsNullOrEmpty(holder) & cvv > 99 & cvv < 1000 & number > 0);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
