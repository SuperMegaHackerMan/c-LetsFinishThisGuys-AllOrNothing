using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.StoreNS
{
    public class Review
    {
        private string description;
        int stars;
        string username;
        public Review(string description, int stars, string username)
        {
            this.description = description;
            this.username = username;
            this.stars = stars;
        }
        public Review(){}
    }
}
