using DataAccess.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.DataAccess
{
    public class DataAccessHandler
    {
        private const string ConnectionString = "mongodb+srv://admin:admin@cluster0.qluhr.mongodb.net/?retryWrites=true&w=majority";
        private const string DatabaseName = "";
        private const string StoreCollection = "store_chart";
        private const string UserCollection = "users";
        private const string ChoreHistoryCollection = "chore_history";

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);

        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var usersCollection = ConnectToMongo<UserDTO>(UserCollection);
            var res = await usersCollection.FindAsync(_ => true);
            return res.ToList();
        }

        public async Task<List<StoreDTO>> GetAllStores()
        {
            var storesCollection = ConnectToMongo<StoreDTO>(StoreCollection);
            var res = await storesCollection.FindAsync(_ => true);
            return res.ToList();
        }

        public async Task<List<StoreDTO>> GetAllStoresForAUser(UserDTO user)
        {
            var storesCollection = ConnectToMongo<StoreDTO>(StoreCollection);
            var res = await storesCollection.FindAsync(s=>s.AssignedTo.Id==user.Id);
            return res.ToList();
        }

        public async Task CreateUser(UserDTO user)
        {
            var usersCollection = ConnectToMongo<UserDTO>(UserCollection);
            await usersCollection.InsertOneAsync(user);
        }

        public async Task CreateStore(StoreDTO store)
        {
            var storesCollection = ConnectToMongo<StoreDTO>(StoreCollection);
            await storesCollection.InsertOneAsync(store);
        }

        public async Task UpdateStore(StoreDTO store)
        {
            var storesCollection = ConnectToMongo<StoreDTO>(ChoreHistoryCollection);
            var filter = Builders<StoreDTO>.Filter.Eq("Id", store.Id);
            await storesCollection.ReplaceOneAsync(filter, store, new ReplaceOptions { IsUpsert = true });
        }

        public async Task DeleteStore(StoreDTO store)
        {
            var storesCollection = ConnectToMongo<StoreDTO>(StoreCollection);
            await storesCollection.DeleteOneAsync(s => s.Id == store.Id);
        }

        //complete chore example 54:09, https://www.youtube.com/watch?v=exXavNOqaVo
        // could work for purchase history
        public async Task CompleteChore(StoreDTO store)
        {
            var storesCollection = ConnectToMongo<StoreDTO>(StoreCollection);
            var filter = Builders<StoreDTO>.Filter.Eq("Id", store.Id);
            await storesCollection.ReplaceOneAsync(filter, store);

            //var storeHistoryCollection = ConnectToMongo<StoreHistoryModel>(storeHistoryCollection);
            //await storeHistoryCollection.InsertOneAsync(new StoreHistoryModel(store));
        }
    }
}
