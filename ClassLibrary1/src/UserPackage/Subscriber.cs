using MarketLib.src.StorePermission;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;
using MarketLib.src.StoreNS;

namespace MarketLib.src.UserP
{
    public class Subscriber: User
    {
        private int id;// will be used fo synchronization (deadlocks).
        private bool isAdmin = false;
        private static int atomicNum;
        private string userName;
        private HashSet<StorePermission.StorePermission> permissions; // synchronized manually
        private ConcurrentDictionary<int ,Product> itemsPurchased; // k: store v: arrylist<product>
        private ArrayList purchaseHistory;

        public Subscriber(string userName)
        {
            this.id = Interlocked.Increment(ref atomicNum);
            this.userName = userName;
            this.permissions = new HashSet<StorePermission.StorePermission>();
            this.itemsPurchased = new ConcurrentDictionary<int, Product>();// k: store v: arrylist<product>
            this.purchaseHistory = new ArrayList();

            //  this.notifications = new ArrayList<>();
        }

        public Subscriber(string userName, HashSet<StorePermission.StorePermission> permissions, ConcurrentDictionary<int, Product> itemsPurchased, ArrayList purchaseHistory)
        {
            this.id = Interlocked.Increment(ref atomicNum);
            this.userName = userName;
            this.permissions = permissions;
            this.itemsPurchased = itemsPurchased;
            this.purchaseHistory = ArrayList.Synchronized(purchaseHistory);
            //this.notifications = new ArrayList<>();
        }

        public string getUserName()
        {
            return userName;
        }

        public Subscriber getSubscriber()
        {
            return this;
        }




        //
        private void addPermission(StorePermission.StorePermission permission)
        {

            lock (permissions)
            {
                permissions.Add(permission);
            }
        }

        //
        private void removePermission(StorePermission.StorePermission permission)
        {

            lock (permissions)
            {
                permissions.Remove(permission);
            }
        }
        //
        public bool havePermission(StorePermission.StorePermission permission)
        {

            lock (permissions)
            {
                foreach(StorePermission.StorePermission perm in permissions)
                {
                   if(permission.Equals(perm) ){
                        return true;
                    }
                }
                return false;
            }
        }
        //
        public void validatePermission(StorePermission.StorePermission permission)
        {

            lock (permissions)
            {//locked so permmisions wont change while checking.מקרה אי דטרמיניסטי
                if (!havePermission(permission))
                    throw new Exception("NoPermissionException: " + permission.ToString());
            }
        }

        public void validateAtLeastOnePermission(ArrayList permissionss/* arraylist<AbsPermmisionss"*/)
        {

            lock (this.permissions)
            {
                foreach (StorePermission.StorePermission per in permissionss)
                {
                    if (havePermission(per))
                        return;
                }
                throw new Exception("NoPermissionException: " + permissionss.ToString());
            }
        }

        /// <summary>
        /// owner choosing a target to make him a manager in his store.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="store"></param>
        public void addManagerPermission(Subscriber target, Store store)
        {

            lock (target.id < id ? target.permissions : permissions)
            {
                lock (target.id < id ? permissions : target.permissions)
                {

                    // check this user has the permission to perform this action
                    validatePermission(new OwnerPermission(store));

                    // check if the target is already a manager at this store
                    StorePermission.StorePermission managerPermission = new ManagerPermission(store);
                    if (target.havePermission(managerPermission))
                        throw new Exception("AlreadyManagerException: " + (userName));

                    // add manager permission to the target
                    target.addPermission(managerPermission);

                    // give the user permission to delete the new permission that was added to the target
                    addPermission(new AppointerPermission(target, store));
                }
            }
        }

        public void removeManagerPermission(Subscriber target, Store store)
        {

            removeOwnerPermission(target, store); // removes all store permissions
        }

        public void addOwnerPermission(Store store)
        {

            lock (permissions)
            {

                addPermission(new OwnerPermission(store));
                addPermission(new ManagerPermission(store));
                addPermission(new ManageInventoryPermission(store));
                //  addPermission(new GetHistoryPermission(store));
            }
        }

        public void addOwnerPermission(Subscriber target, Store store)
        {

            lock (target.id < id ? target.permissions : permissions)
            {
                lock (target.id < id ? permissions : target.permissions)
                {

                    // check this user has the permission to perform this action
                    StorePermission.StorePermission ownerPermission = new OwnerPermission(store);
                    validatePermission(ownerPermission);

                    // check if the target is already an owner at this store
                    if (target.havePermission(ownerPermission))
                        throw new Exception("AlreadyOwnerException: " + (userName));

                    // check if the target is a manager that was appointed by someone else
                    ManagerPermission managerPermission = new ManagerPermission(store);
                    if (target.havePermission(managerPermission))
                        validatePermission(new AppointerPermission(target, store));

                    target.addOwnerPermission(store);

                    // give the user permission to delete the new permission that was added to the target
                    addPermission(new AppointerPermission(target, store));
                }
            }
        }

        public void removeOwnerPermission(Store store)
        {

            lock (permissions)
            {

                ArrayList permissionsToRemove = new ArrayList(); // ARRAYLIST<Abspermission>

                // look for any managers or owners that were appointed by this owner for this store and remove their permission
                foreach (StorePermission.StorePermission per in permissions)
                    if (per.GetType().Name == typeof(AppointerPermission).Name && ((AppointerPermission)per).getStore() == store)
                    {
                        Subscriber target = ((AppointerPermission)per).getTarget();
                        target.removeOwnerPermission(store);

                        permissionsToRemove.Add(per); // store this permission to remove it after the foreach loop
                    }

                foreach (StorePermission.StorePermission per in permissionsToRemove)
                {
                    if (permissions.Contains(per))
                    {
                        permissions.Remove(per);
                    }
                }

                permissionsToRemove.Clear();

                removePermission(new OwnerPermission(store));
                //removePermission(EditPolicyPermission.getInstance(store));
                removePermission(new ManageInventoryPermission(store));
                removePermission(new ManagerPermission(store));
            }
        }

        public void removeOwnerPermission(Subscriber target, Store store)
        {

            // lock (target.id < id ? target.permissions : permissions) {
            // lock (target.id < id ? permissions : target.permissions) {

            // check this user has the permission to perform this action
            validatePermission(new AppointerPermission(target, store));

            target.removeOwnerPermission(store);

            // remove this user's permission to change the target's permissions
            removePermission(new AppointerPermission(target, store));

        }

        public void addInventoryManagementPermission(Subscriber target, Store store)
        {

            addPermissionToManager(target, store, new ManageInventoryPermission(store));
        }

        public void removeInventoryManagementPermission(Subscriber target, Store store)
        {

            removePermissionFromManager(target, store, new ManageInventoryPermission(store));
        }


        public void addPermissionToManager(Subscriber target, Store store, StorePermission.StorePermission permission)
        {

            //  lock (target.id < id ? target.permissions : permissions) {
            // lock (target.id < id ? permissions : target.permissions) {

            // check this user has the permission to perform this action
            validatePermission(new AppointerPermission(target, store));

            if (!target.havePermission(new ManagerPermission(store)))
                throw new Exception("TargetIsNotManagerException:  " + "username : " + target.getUserName() + ", store: " + store.getName());

            // add the permission to the target (if he doesn't already have it)
            target.addPermission(permission);
            // }
            // }
        }

        public void removePermissionFromManager(Subscriber target, Store store, StorePermission.StorePermission permission)
        {

            //   lock (target.id < id ? target.permissions : permissions) {
            //lock (target.id < id ? permissions : target.permissions) {

            // check this user has the permission to perform this action
            validatePermission(new AppointerPermission(target, store));

            if (target.havePermission(new OwnerPermission(store)))
                throw new Exception("TargetIsNotOwnerException:  " + "username : " + target.getUserName() + ", store: " + store.getName());

            target.removePermission(permission);
            //    }
            // }
        }

        public int addStoreItem(Store store, string itemName, string category, string subCategory, int quantity, double price)
        {
            lock (permissions)
            {
                // check this user has the permission to perform this action
                validatePermission(new ManageInventoryPermission(store));

                return store.addItem(itemName, price, category, subCategory, quantity);
            }
        }

        public void removeStoreItem(Store store, int itemId)
        {
            lock (permissions)
            {
                // check this user has the permission to perform this action
                validatePermission(new ManageInventoryPermission(store));

                store.removeItem(itemId);
            }
        }

        public void updateStoreItem(Store store, int itemId, string newSubCategory, int newQuantity, double newPrice)
        {

            // check this user has the permission to perform this action
            validatePermission(new ManageInventoryPermission(store));

            store.changeItem(itemId, newQuantity, newPrice);
        }


        public string storePermissionsToString(Store store)
        {

            lock (permissions)
            {

                StringBuilder result = new StringBuilder();

                StorePermission.StorePermission ownerPermission = new OwnerPermission(store);
                StorePermission.StorePermission managerPermission = new ManagerPermission(store);
                StorePermission.StorePermission manageInventoryPermission = new ManageInventoryPermission(store);
                //AbsStorePermission getHistoryPermission = new GetHistoryPermission(store);
                //Permission editPolicyPermission = EditPolicyPermission.getInstance(store);

                if (havePermission(ownerPermission))
                    result.Append(ownerPermission.toString()).Append(" ");
                if (havePermission(managerPermission))
                    result.Append(managerPermission.toString()).Append(" ");
                if (havePermission(manageInventoryPermission))
                    result.Append(manageInventoryPermission.toString()).Append(" ");
                //if (havePermission(getHistoryPermission))
                //   result.Append(getHistoryPermission.toString()).Append(" ");
                //if (havePermission(editPolicyPermission))
                // result.Append(editPolicyPermission.toString()).Append(" ");

                return result.ToString();
            }
        }

    }
}
