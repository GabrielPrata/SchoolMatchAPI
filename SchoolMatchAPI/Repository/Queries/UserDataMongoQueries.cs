using AccountService.Model.MongoModels;
using MongoDB.Driver;
namespace AccountService.Repository.Queries
{
    public class UserDataMongoQueries
    {
        private readonly IMongoCollection<MongoUserData> _userCollection;


        public UserDataMongoQueries(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("UsersData");
            _userCollection = database.GetCollection<MongoUserData>("UsersData");
        }

        public async Task<MongoUserData?> GetUserById(int userId)
        {
            try
            {
                var filter = Builders<MongoUserData>.Filter.Eq(u => u.IdUsuario, userId);
                return await _userCollection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task SaveUserData(MongoUserData data)
        {
            await _userCollection.InsertOneAsync(data);
        }
    }
}
