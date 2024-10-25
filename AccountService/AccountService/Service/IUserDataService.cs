using AccountService.Data.DTO;

namespace AccountService.Service
{
    public interface IUserDataService
    {

        Task<UserDataDTO> GetUserDataById(int userId);
        Task SaveUserData(UserDataDTO userData);
    }
}
