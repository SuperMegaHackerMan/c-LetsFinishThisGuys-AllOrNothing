using MarketLib.src.MarketSystemNS;
using MarketLib.src.StoreNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.Service
{
    public class MarketSystemServiceImpl
    {
        private MarketSystem market;


        public string connect()
        {
            return market.connect();
        }

        public void exit(string connectionid)
        {
            market.exit(connectionid);
        }

        public void register(string connectionID, string userName, string password)
        {
            market.register(connectionID, userName, password);
        }

        public string login(string connectionid, string userName, string pass)
        {
            return market.login(connectionid, userName, pass);
        }

        public void logout(string username)
        {
            market.logout(username);
        }

        public int bidOnItemAsBuyer(string connectionId, int storeId, string product_name, double price, string category, string subcat)
        {
            return market.bidOnItemAsBuyer(connectionId, storeId, product_name, price, category, subcat);
        }

        public void acceptBidAsManager(string connectionId, int bidId)
        {
            market.acceptBidAsManager(connectionId, bidId);
        }

        public void declineBidAsManager(string connectionId, int bidId)
        {
            market.acceptBidAsManager(connectionId, bidId);
        }

        public void counterOfferAsManager(string connectionId, int bidId, double newPrice)
        {
            market.counterOfferAsManager(connectionId, bidId, newPrice);
        }

        public void acceptCounterOfferAsBuyer(string connectionId, int bidId)
        {
            market.acceptCounterOfferAsBuyer(connectionId, bidId);
        }

        public void declineCounterOfferAsBuyer(string connectionId, int bidId)
        {
            market.declineCounterOfferAsBuyer(connectionId, bidId);
        }

        public int openNewStore(string username, string newStoreName)
        {
            return market.openNewStore(username, newStoreName);
        }

        public void appointStoreManager(string username, string assigneeUserName, int storeId)
        {
            market.appointStoreManager(username, assigneeUserName, storeId);
        }

        public void addProductToStore(string username, int storeId, string productName, string category, string subCategory,
            int quantity, double price)
        {
            market.addProductToStore(username, storeId, productName, category, subCategory, quantity, price);
        }

        public void deleteProductFromStore(string username, int storeId, int productID)
        {
            market.deleteProductFromStore(username, storeId, productID);
        }

        public void updateProductDetails(int storeid, string username, int productID, string newSubCategory, int newQuantity, double newPrice)
        {
            market.updateProductDetails(storeid, username, productID, newSubCategory, newQuantity, newPrice);
        }

        public void appointStoreOwner(string username, string assigneeUserName, int storeId)
        {
            market.appointStoreOwner(username, assigneeUserName, storeId);
        }

        public void giveManagerUpdateProductsPermmission(string username, int storeId, string managerUserName)
        {
            market.giveManagerUpdateProductsPermmission(username, storeId, managerUserName);
        }

        public void takeManagerUpdatePermmission(string username, int storeId, string managerUserName)
        {
            market.takeManagerUpdatePermmission(username, storeId, managerUserName);
        }

        public ICollection<Product> filterProducts(int store_rating, string name, string category, double startprice, double endprice, double rating)
        {
            return market.filterProducts(store_rating, name, category, startprice, endprice, rating);
        }

        public void giveManagerGetHistoryPermmision(string username, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public void takeManagerGetHistoryPermmision(string username, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public bool removeManager(string username, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public bool removeOwner(string username, string storeId, string targetUserName)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<string> showStaffInfo(string username, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<string> getStoreHistory(string username, string storeId)
        {
            throw new System.NotImplementedException();
        }
    }
    
}

