using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class SubscriberDTO
    {
        private int id;// will be used fo synchronization (deadlocks).
        private bool isAdmin = false;
        private static int atomicNum;
        private string userName;
        private HashSet<StorePermission.StorePermission> permissions; // synchronized manually
        private ConcurrentDictionary<int, ProductDTO> itemsPurchased; // k: store v: arrylist<product>
        private ArrayList purchaseHistory;
    }
}
