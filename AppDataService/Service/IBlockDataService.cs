using AccountService.Data.DTO;
using AppDataService.Data.DTO;

namespace AccountService.Service
{
    public interface IBlockDataService
    {

        Task<List<BlocksDTO>> GetMainBlocks();
        Task<List<BlocksDTO>> GetSecondaryBlocks();

    }
}
