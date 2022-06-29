using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MarketLib.src.Data.Models
{
    public class StoreModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        //private PurchasePolicy purchase_policy;
        //private DiscountPolicy discount_policy;
        private int Id { get; set; }
        private string name { get; set; }
        private string description { get; set; }
        private double rating { get; set; }
        private string founderUserName { get; set; }
        private bool isActive { get; set; }
        private int managerCounter { get; set; }
        //private ConcurrentDictionary<int, User> managers;
        //private Inventory inventory = new Inventory();
        //private ArrayList purchases = new ArrayList(); 
    }
}
