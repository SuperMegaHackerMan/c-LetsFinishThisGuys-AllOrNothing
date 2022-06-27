
using MarketLib.src.ExternalService.Payment;
using MarketLib.src.ExternalService.Supply;
using MarketLib.src.Security;
using MarketLib.src.StoreNS;

using MarketLib.src.UserP;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace MarketLib.src.MarketSystemNS
{
    public class MarketSystem
    {
        private ConcurrentDictionary<string, User> connections;
        private ConcurrentDictionary<string, Subscriber> members;
        private ConcurrentDictionary<int, Store> stores; // key: store id
        private int storeCounter = 0;
        private ItemSearchManager search = new ItemSearchManager();
        private UserSecurity usersecure = new UserSecurity();
        private int storeIdCounter = 0;
        private int counterId = 0;
        private PaymentSystem payment;
        private SupplySystem supply;
        


        public Subscriber getSubscriberByUserName(string userName)
        {
            Subscriber subscriber = members[userName];
            if (subscriber == null) throw new Exception(userName);
            return subscriber;
        }


        public MarketSystem()
        {
            connections = new ConcurrentDictionary<string, User>();
            members = new ConcurrentDictionary<string, Subscriber>();
            stores = new ConcurrentDictionary<int, Store>();
            //payment = new PaymentSystemImpl(new PaymentAdapter());
            supply = new SupplySystemImpl(new DeliveryAdapter());

        }


        public string connect()
        {
            string unqid = Guid.NewGuid().ToString();//generates a random user id.
            connections.TryAdd(unqid, new User());
            return unqid;
        }

        public void exit(string connectid)
        {
            User v;
            connections.TryRemove(connectid, out v);
        }

        public void register(string connectionid, string username, string password)
        {
            if (connections.ContainsKey(connectionid)) //if you are a vistor
            {
                if (members.ContainsKey(username))//if this username already exists.
                    throw new Exception("this username already exists");
                Subscriber member = new Subscriber(username);
                members[username] = member;
                usersecure.storeUser(username, password);
            }

        }

        //when a user logs in we return his username to use as an identifier
        //when we will have a data base this will maybe look al different.
        public string login(string connection, string username, string password)
        {
            if (connections.ContainsKey(connection) && members.ContainsKey(username))
            {
                usersecure.verifyUser(username, password); //verify user info 
                connections[connection] = members[username]; //with thgis we can use the user key to attain access to a memeber.
                return username;
            }
            throw new Exception();
        }

        public ICollection<Product> filterProducts( int store_rating, string name, string category, double startprice, double endprice, double rating)
        {
            return search.filterProducts(stores.Values, store_rating, name, category, startprice, endprice, rating);
        }

        //the logouy the user from the system and will retun hum as a new vistor
        //in this function we infer that the username value in the presentation/service layer is
        //back to null.
        public void logout(string connectionId)
        {
            User guest = new User();
            connections[connectionId] = guest;//with this we remove the permission to attain access to the subscriber.
        }

        /// <summary>
        /// gets info of all stores in the system,
        /// will  be used to make a dynamic gui.
        /// </summary>
        /// <returns>a collection of stores</returns>
        public ICollection<Store> StoresInfo()
        {
            ICollection<Store> stores = this.stores.Values;
            return stores;

        }//TODO: we should make a store controller class which has all the functionality that store has 
         // and make store a data class with superficial data.


        /// <summary>
        /// adds an item into the right basket
        /// NOTE: this is before the purchase therfore he can take any quantity as he likes.
        /// </summary>
        /// <param name="userID">the connection id</param>
        /// <param name="storeId">the id of the store we are adding from </param>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        public void addItemToBasket(string connectionID, int storeId, int productId, int amount)
        {
            User user = getUserByConnectionId(connectionID);
            Store store = stores[storeId];
            Product p = store.searchItemById(productId);
            user.getBasket(storeId).addProduct(p, amount);
        }

        public User getUserByConnectionId(string connectionId)
        {
            User user = connections[connectionId];
            if (user == null)
                throw new Exception("non such user exists");
            return user;
        }

        /// <summary>
        /// will show the cart of the user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>the users cart</returns>
        public ConcurrentDictionary<int, Basket> showCart(string userID)//TODO: 1.chamge the interface 2.should return a <store, basket> and not store id. 
        {
            User user = getUserByConnectionId(userID);
            return user.getCart();
        }

        /// <summary>
        /// update a product amount in a specific store basket.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="storeId"></param>
        /// <param name="productId"></param>
        /// <param name="newAmount"></param>
        public void updateProductAmountInBasket(string connectionID, int storeId, int productId, int newAmount)
        {
            User user = getUserByConnectionId(connectionID);
            Basket basket = user.getBasket(storeId);
            Store store = stores[storeId];
            Product p = store.searchItemById(productId);
            basket.setQuantity(p, newAmount);
        }

        /// <summary>
        /// makes the purchase of the cart. this method should be thread safe
        /// to not mistake in the product quantity.
        /// a purchase record should also be made for each basket(store) and user.(//TODO: think how to make purchase records in the system). 
        /// </summary>
        /// <param name="userID"></param>
        public void purchaseCart(string connectionID)
        {
            User user = getUserByConnectionId(connectionID);
            ConcurrentDictionary<int, Basket> baskets = user.getCart();
            double totalprice = 0;
            try ///first try the external systems , then check if the purchase itslef is valid 
            {
                foreach (KeyValuePair<int, Basket> entry in baskets)
                {
                    Store store = stores[entry.Key];
                    double store_total = store.calculateBasket(user.getBasket(store.getId()));
                    totalprice += store_total;
                    PurchaseRecord purchaseDetails = new PurchaseRecord(user.getBasket(store.getId()).Products, DateTime.Now.ToString("dd-MM-yyyy"), store_total);
                }
            }
            catch
            {
                throw new Exception("illegal purchase");
            }
        }

        /// <summary>
        /// user writes his review for a product he  bought.
        /// we need to check that the store actually has a product with that id
        /// and that the user actually purchased those items (from looking at his purchase history).
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="storeID"></param>
        /// <param name="productId"></param>
        /// <param name="desc"></param>
        // public void writeOpinionOnProduct(string username, int storeID, string productId, string desc, int stars)
        // {
        //   Subscriber user = getSubscriberByUserName(username); //check if this is allowed to guest too...
        //    Store store = stores[storeID];
        //   s

        // }

        /// <summary>
        /// a member makes a new store.
        /// </summary>
        /// <param name="username"> the members username</param>
        /// <param name="newStoreName"></param>
        /// <returns>return the storeid</returns>
        public int openNewStore(string username, string newStoreName)
        {
            Subscriber user = getSubscriberByUserName(username);
            Store newStore = new Store(storeCounter, newStoreName, username);
            stores.TryAdd(storeCounter,newStore);
            int current = storeCounter;
            Interlocked.Increment(ref storeCounter);
            return current;
        }

        /// <summary>
        /// apoint a new member to the store as a store Manager.
        /// the member has to be somone who doesnt already has Mangmenr/Ownership permissions.
        /// the manager has one appointer which is a store owner and he only got permmsion to recieve data
        /// of the store.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="assigneeUserName"></param>
        /// <param name="storeId"></param>
        public void appointStoreManager(string username, string assigneeUserName, int storeId)
        {
            Subscriber user = getSubscriberByUserName(username);
            Subscriber appointed = getSubscriberByUserName(assigneeUserName);
            Store s = stores[storeId];
            user.addManagerPermission(appointed, s);
        }

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
        public void addProductToStore(string username, int storeId, string productName, string category, string subCategory,
         int quantity, double price)
        {
            Subscriber user = getSubscriberByUserName(username);
            Store s = stores[storeId];
            user.addStoreItem(s, productName, category, subCategory, quantity, price);
        }


        public void deleteProductFromStore(string username, int storeId, int productID)
        {
            Subscriber user = getSubscriberByUserName(username);
            Store s = stores[storeId];
            user.removeStoreItem(s, productID);
        }

        public void updateProductDetails(int storeid, string username, int productID, string newSubCategory, int newQuantity, double newPrice)
        {
            Subscriber user = getSubscriberByUserName(username);
            Store s = stores[storeid];
            user.updateStoreItem(s, productID, newSubCategory, newQuantity, newPrice);
        }

        public void appointStoreOwner(string username, string assigneeUserName, int storeId)
        {
            Subscriber user = getSubscriberByUserName(username);
            Store s = stores[storeId];
            user.addOwnerPermission(s);
        }

        public void giveManagerUpdateProductsPermmission(string username, int storeId, string managerUserName)
        {
            Subscriber user = getSubscriberByUserName(username);
            Subscriber target = getSubscriberByUserName(managerUserName);
            Store s = stores[storeId];
            user.addInventoryManagementPermission(target, s);
        }

        public void takeManagerUpdatePermmission(string username, int storeId, string managerUserName)
        {
            Subscriber user = getSubscriberByUserName(username);
            Subscriber target = getSubscriberByUserName(managerUserName);
            Store s = stores[storeId];
            user.removeManagerPermission(target, s);
        }



    }
}
