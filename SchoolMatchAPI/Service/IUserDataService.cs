using AccountService.Data.DTO;

namespace AccountService.Service
{
    public interface IUserDataService
    {
        Task<UserDataDTO> ValidateLogin(UserLoginDTO loginData);
        Task<UserDataDTO> GetUserDataById(int userId);
        Task SaveUserData(UserDataDTO userData);
        Task UpdateUserData(UserDataDTO userData);
        Task SaveEmailToVerify(string userEmail);
        Task<bool> CheckIfEmailIsVerified(string userEmail);


        //Task<SQLUserDataDTO> FindUserSqlById(string userId);
        //Task<MongoUserDataDTO> FindUserMongoById(string userId);

        //Task<SQLUserDataDTO> InsertUserSql(SQLUserDataDTO data);
        //Task<MongoUserDataDTO> InsertUserMongo(MongoUserDataDTO data);

        //Task<SQLUserDataDTO> UpdateUserSql(SQLUserDataDTO data);
        //Task<MongoUserDataDTO> UpdateUserMongo(MongoUserDataDTO data);

        //Task<SQLUserDataDTO> DeleteUserSql(string userId);
        //Task<MongoUserDataDTO> DeleteUserMongo(string userId);
    }
}
