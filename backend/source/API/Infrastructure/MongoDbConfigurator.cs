using MongoDB.Driver;

namespace OriolOr.Maneko.API.Infrastructure
{
    public static class MongoDbConfigurator
    {
        public static IMongoClient? MongoClient;
        public static IMongoDatabase? DataBase;

        public static void InitializeDataBase() {

            MongoClient = new MongoClient("mongodb://localhost:27017");
            DataBase = MongoClient.GetDatabase("ManekoDataBase");
    
            UserDataRepository userDataRepository = new UserDataRepository();
            BalanceRepository balanceRepository = new BalanceRepository();
            try
            {
                InitializeMongoDbCollection(DataBase, balanceRepository.CollectionName);
                InitializeMongoDbCollection(DataBase, userDataRepository.CollectionName);

                userDataRepository.LoadUserData(DataBase);
            }
            catch{}

        }

        private static void InitializeMongoDbCollection(IMongoDatabase dataBase, string collectionName)
        {
            var collectionsList = dataBase.ListCollectionNames().ToList();

            if (!collectionsList.Contains(collectionName)) dataBase.CreateCollection(collectionName);
        }

    }
}