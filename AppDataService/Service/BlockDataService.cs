using AccountService.Data.DTO;
using AccountService.Mappers;
using AccountService.Model.SqlModels;
using AccountService.Repository;
using AccountService.Service;
using AppDataService.Data.DTO;
using AppDataService.Mappers;
using AppDataService.Model.SqlModels;
using AppDataService.Repository;
using Microsoft.Data.SqlClient;

namespace AppDataService.Service
{
    public class BlockDataService : IBlockDataService
    {
        private readonly BlocksDataRepository _blockRepository;

        public BlockDataService(SqlConnection sqlConnection)
        {
            _blockRepository = new BlocksDataRepository(sqlConnection);
        }

        public async Task<List<BlocksDTO>> GetMainBlocks()
        {
            var data = await _blockRepository.GetMainBlocks();

            List<BlocksDTO> dataDTO = new List<BlocksDTO>();

            foreach (SqlBlockData bloco in data)
            {
                dataDTO.Add(BlocksMapper.ToDto(bloco));
            }

            return dataDTO;
        }

        public async Task<List<BlocksDTO>> GetSecondaryBlocks()
        {
            var data = await _blockRepository.GetSecondaryBlocks();

            List<BlocksDTO> dataDTO = new List<BlocksDTO>();

            foreach (SqlBlockData bloco in data)
            {
                dataDTO.Add(BlocksMapper.ToDto(bloco));
            }

            return dataDTO;
        }
    }
}
