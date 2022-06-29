using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    internal class InventoryDTO
    {
        private Dictionary<ProductDTO, int> products; 
        private int idpatcher;
    }
}
