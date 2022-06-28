using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.Service
{
   
        public interface IMarketSystemService
        {



            /// <summary>
            /// connects the visitor to the system and return his connection id
            /// that is used to help other functionality in the system.
            /// user requirment 1.1
            /// </summary>
            /// <returns></returns>
            string connect();

            /// <summary>
            /// function used when a user exits the system (like pressing the x button)
            /// it will get rid of the visitor who had the same connection id.
            /// if it was a member such a method is not supposed to delete him from the system.
            /// </summary>
            void exit(string connectionid);

            /// <summary>
            /// registers a new member into the system.
            /// is supposed to work only if the current user is a visitor.
            /// visitor 
            /// </summary>
            /// <param name="userName"></param>
            /// <param name="password"></param>
            void register(string connectionId, string userName, string password);

            /// <summary>
            /// logins into the system
            /// should be an option only for a vistor.
            /// will return the username if successful.
            /// </summary>
            /// <param name="connectID"></param>
            /// <param name="userName"></param>
            /// <param name="pass"></param>
            string login(string connectionid, string userName, string pass);

            /// <summary>
            /// logouts from the system.
            /// a function that should be available only for members 
            /// </summary>
            /// <param name="username"></param>
            void logout(string username);

            /// <summary>
            /// gets info of all stores in the system,
            /// will  be used to make a dynamic gui.
            /// </summary>
            /// <returns>a collection of stores</returns>
            ICollection<Store> StoresInfo();//TODO: we should make a store controller class which has all the functionality that store has 
                                            // and make store a data class with superficial data.


            /// <summary>
            /// adds an item into the right basket
            /// </summary>
            /// <param name="userID">the connection id</param>
            /// <param name="storeId">the id of the store we are adding from </param>
            /// <param name="productId"></param>
            /// <param name="amount"></param>
            void addItemToBasket(string connectionID, string storeId, string productId, int amount);

            /// <summary>
            /// will show the cart of the user
            /// </summary>
            /// <param name="userID"></param>
            /// <returns>the users cart</returns>
            ICollection<Basket> showCart(string userID); //TODO: same as in store, we need a data class and a controller class.


            /// <summary>
            /// update a product amount in a specific store basket.
            /// </summary>
            /// <param name="userID"></param>
            /// <param name="storeId"></param>
            /// <param name="productId"></param>
            /// <param name="newAmount"></param>
            void updateProductAmountInBasket(string connectionID, string storeId, string productId, int newAmount);

            /// <summary>
            /// makes the purchase of the cart. this method should be thread safe
            /// to not mistake in the product quantity.
            /// a purchase record should also be made for each basket(store) and user.(//TODO: think how to make purchase records in the system). 
            /// </summary>
            /// <param name="userID"></param>
            void purchaseCart(string userID);

            /// <summary>
            /// get the purchase history of a user, only the same user has the permmision to do so
            /// and the store Manager.
            /// </summary>
            /// <param name="userID"></param>
            /// <returns></returns>
            //Collection<string> getPurchaseHistory(string userID);TODO: think how to make purchase records in the system). 


            /// <summary>
            /// user writes his review for a product he  bought.
            /// we need to check that the store actually has a product with that id
            /// and that the user actually purchased those items (from looking at his purchase history).
            /// </summary>
            /// <param name="connectionId"></param>
            /// <param name="storeID"></param>
            /// <param name="productId"></param>
            /// <param name="desc"></param>
            void writeOpinionOnProduct(string connectionID, string storeID, string productId, string desc);


            /// <summary>
            /// a member makes a new store.
            /// </summary>
            /// <param name="username"> the members username</param>
            /// <param name="newStoreName"></param>
            /// <returns>return the storeid</returns>
            int openNewStore(string username, string newStoreName);

            /// <summary>
            /// apoint a new member to the store as a store Manager.
            /// the member has to be somone who doesnt already has Mangmenr/Ownership permissions.
            /// the manager has one appointer which is a store owner and he only got permmsion to recieve data
            /// of the store.
            /// </summary>
            /// <param name="username"></param>
            /// <param name="assigneeUserName"></param>
            /// <param name="storeId"></param>
            void appointStoreManager(string username, string assigneeUserName, int storeId);

            /// <summary>
            /// add a new type of product to the store with its details.
            /// only a owner/manager can have the permmision to do so.
            /// </summary>
            /// <param name="username"></param>
            /// <param name="storeId"></param>
            /// <param name="productName"></param>
            /// <param name="category"></param>
            /// <param name="subCategory"></param>
            /// <param name="quantity"></param>
            /// <param name="price"></param>
            void addProductToStore(string username, int storeId, string productName, string category, string subCategory,
             int quantity, double price);


            /// <summary>
            /// deletes a product from a store
            /// invoker is the store owner/manager  with permissions to make changes in products.
            /// </summary>
            /// <param name="username"></param>
            /// <param name="storeId"></param>
            /// <param name="productID"></param>
            void deleteProductFromStore(string username, int storeId, string productID);

            /// <summary>
            /// deletes a product from a store
            /// invoker is the store owner/manager  with permissions to make changes in products.
            /// </summary>
            void updateProductDetails(int storeid, string username, int productID, string newSubCategory, int newQuantity, double newPrice);


            /// <summary>
            /// appoints a new store owner
            /// </summary>
            /// <param name="username">the owner</param>
            /// <param name="assigneeUserName"></param>
            /// <param name="storeId"></param>
            void appointStoreOwner(string username, string assigneeUserName, int storeId);



            /// <summary>
            /// give a manager the permmision to update the product data.
            /// only manage who appointed him can give him this permission
            /// </summary>
            /// <param name="username">the owner</param>
            /// <param name="storeId"></param>
            /// <param name="managerUserName"></param>
            void giveManagerUpdateProductsPermmission(string username, int storeId, string managerUserName);

            /// <summary>
            /// take from manager the permmision to update the product data.
            /// only manage who appointed him can disable this permission.
            /// </summary>
            /// <param name="username">the owner</param>
            /// <param name="storeId"></param>
            /// <param name="managerUserName"></param>
            void takeManagerUpdatePermmission(string username, string storeId, string managerUserName);


            /// <summary>
            /// allows manager to get purchases history of the store. same conditions...
            /// </summary>
            /// <param name="username">the owner</param>
            /// <param name="storeId"></param>
            /// <param name="managerUserName"></param>
            void giveManagerGetHistoryPermmision(string username, string storeId, string managerUserName);


            /// <summary>
            /// disables a manager from getting purchases history of the store. same conditions...
            /// </summary>
            /// <param name="username">the owner</param>
            /// <param name="storeId"></param>
            /// <param name="managerUserName"></param>
            void takeManagerGetHistoryPermmision(string username, string storeId, string managerUserName);

            /// <summary>
            /// removes a manager from the store. only the owner who apointed the
            /// manaager can get rid of him.
            /// </summary>
            /// <param name="username">the owner</param>
            /// <param name="storeId"></param>
            /// <param name="managerUserName"></param>
            /// <returns></returns>
            bool removeManager(string username, string storeId, string managerUserName);

            /// <summary>
            /// removes a manager from the store. only the owner who apointed the
            /// manaager can get rid of him.
            /// </summary>
            /// <param name="username"></param>
            /// <param name="storeId"></param>
            /// <param name="managerUserName"></param>
            /// <returns></returns>
            bool removeOwner(string username, string storeId, string targetUserName);


            /// <summary>
            /// shows store staff information and their permissions in the store
            /// TODO: UNDERSTAND WHAT THEY WANT IN 4.11 for now it  returns stirng .
            /// </summary>
            /// <param name="username"></param>
            /// <param name="storeId"></param>
            /// <returns></returns>
            ICollection<string> showStaffInfo(string username, string storeId);


            /// <summary>
            /// get the purchase history of a store by its id.
            ///  req 6.4 and 4.13.
            /// TODO: nee to think how to ass the history , maybe a list of purchases in the store class and user class or
            /// an assication class of history which keeps all the purchase data.
            /// </summary>
            /// <param name="username"></param>
            /// <param name="storeId"></param>
            /// <returns></returns>
            ICollection<string> getStoreHistory(string username, string storeId);
        }
    
}
