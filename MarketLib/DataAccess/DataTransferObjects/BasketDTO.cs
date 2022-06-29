using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class BasketDTO
    {
        private int storeid;
        private ConcurrentDictionary<ProductDTO, int> products; 
        public ConcurrentDictionary<ProductDTO, int> Products;
    }
}
