using AccountService.Data.DTO;
using AccountService.Repository.Queries;
using AccountService.Mappers;

namespace AccountService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDataSqlQueries _sqlQueries;
        private readonly UserDataMongoQueries _mongoQueries;

        public UserRepository(string sqlConnection, string mongoConnection)
        {
            _sqlQueries = new UserDataSqlQueries(sqlConnection);
            _mongoQueries = new UserDataMongoQueries(mongoConnection);

        }
        public async Task<UserDataDTO> GetUserDataById(int userId)
        {
            try
            {
                #region Recuperando os dados do SQL
                var sqlModel = await _sqlQueries.GetUserDataById(userId);
                var mongoModel = await _mongoQueries.GetUserById(userId);



                if (sqlModel != null && mongoModel != null)
                {
                   var model = UserMapper.ToDto(sqlModel, mongoModel);
                   return model;
                }
                #endregion

                return null;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public Task SaveUserData(UserDataDTO userData)
        {
            throw new NotImplementedException();
        }
    }
}
