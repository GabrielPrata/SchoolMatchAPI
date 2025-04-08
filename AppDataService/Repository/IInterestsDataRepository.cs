using AccountService.Model.SqlModels;
using AppDataService.Model.SqlModels;

namespace AppDataService.Repository
{
    public interface IInterestsDataRepository
    {
        Task<IEnumerable<SqlInterestsData>> GetAllInterests();
    }
}
