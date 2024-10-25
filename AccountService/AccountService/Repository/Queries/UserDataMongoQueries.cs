using AccountService.Data.DTO;
using AccountService.Model;
using Microsoft.Data.SqlClient;
using MongoDB.Driver;

namespace AccountService.Repository.Queries
{
    public class UserDataMongoQueries
    {
        private readonly string _connectionString;
        public UserDataMongoQueries(string strConnection)
        {
            _connectionString = strConnection;
        }

        private MongoClient GetOpenClient()
        {
            var client = new MongoClient(_connectionString);
            return client;
        }

        public async Task<MongoUserData?> GetUserById(int userId)
        {
            var client = GetOpenClient();
            var database = client.GetDatabase("UsersData");
            var collection = database.GetCollection<MongoUserData>("UsersData");
            var filter = Builders<MongoUserData>.Filter.Eq(u => u.IdUsuario, userId);
            var usuario = collection.Find(filter).FirstOrDefault();
            if (usuario != null)
            {
                Console.WriteLine("Usuário encontrado: " + usuario.Nome);
            }
            else
            {
                Console.WriteLine("Usuário não encontrado.");
            }

            return usuario;
        }
       
    }
}
