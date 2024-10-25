using AccountService.Service;
using AccountService.Data.DTO;
using AccountService.Repository;

namespace AccountService.Services;

public class UserDataService : IUserDataService
{
    private readonly UserRepository _userRepository;

    public UserDataService(string sqlConnection, string mongoConnection)
    {
        _userRepository = new UserRepository(sqlConnection, mongoConnection);
    }

    public async Task<UserDataDTO> GetUserDataById(int userId)
    {
        var data = await _userRepository.GetUserDataById(userId);

        //Deixei esta camada para caso seja necessário aplicar alguma "regra de negócio"
        return data;
    }

    public Task SaveUserData(UserDataDTO userData)
    {
        throw new NotImplementedException();
    }
}