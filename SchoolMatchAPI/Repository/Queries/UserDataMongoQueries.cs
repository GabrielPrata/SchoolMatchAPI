using AccountService.Model.MongoModels;
using MongoDB.Driver;
namespace AccountService.Repository.Queries
{
    public class UserDataMongoRepository
    {
        private readonly IMongoCollection<MongoUserData> _userCollection;


        public UserDataMongoRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("UsersData");
            _userCollection = database.GetCollection<MongoUserData>("UsersData");
        }

        public async Task<MongoUserData?> GetUserById(int userId)
        {
            var filter = Builders<MongoUserData>.Filter.Eq(u => u.IdUsuario, userId);
            return await _userCollection.Find(filter).FirstOrDefaultAsync();
            
        }

        public async Task SaveUserData(MongoUserData data)
        {
            await _userCollection.InsertOneAsync(data);
        }

        public async Task UpdateUserData(MongoUserData data)
        {
            var filter = Builders<MongoUserData>.Filter.Eq(u => u._id, data._id);
            await _userCollection.ReplaceOneAsync(filter, data);
        }
    }
}
