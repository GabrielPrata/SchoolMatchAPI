using AccountService.Model.SqlModels;
using AppDataService.Model.SqlModels;

namespace AppDataService.Repository
{
    public interface IBlocksDataRepository
    {
        Task<IEnumerable<SqlBlockData>> GetMainBlocks();
        Task<IEnumerable<SqlBlockData>> GetSecondaryBlocks();
    }
}
