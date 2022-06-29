using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.DataTransferObjects
{
    public class StoreDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private PurchasePolicyDTO purchase_policy;
        private DiscountPolicyDTO discount_policy;
        private int Id { get; set; }
        private string name { get; set; }
        private string description { get; set; }
        private double rating { get; set; }
        private string founderUserName { get; set; }
        private bool isActive { get; set; }
        private int managerCounter { get; set; }
        private ConcurrentDictionary<int, UserDTO> managers;
        private InventoryDTO inventory = new InventoryDTO();
        private ArrayList purchases = new ArrayList(); 
    }
}
