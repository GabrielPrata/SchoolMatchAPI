using AccountService.Model.SqlModels;
using AppDataService.Model.SqlModels;

namespace AppDataService.Repository
{
    public interface ISexualityDataRepository
    {
        Task<IEnumerable<SqlSexualityData>> GetSexualities();
    }
}
