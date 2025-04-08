using AccountService.Repository;
using AppDataService.Data.DTO;
using AppDataService.Mappers;
using AppDataService.Model.SqlModels;
using AppDataService.Repository;
using Microsoft.Data.SqlClient;

namespace AppDataService.Service
{
    public class SexualityDataService : ISexualityDataService
    {
        private readonly SexualityDataRepository _sexualityRepository;

        public SexualityDataService(SqlConnection sqlConnection)
        {
            _sexualityRepository = new SexualityDataRepository(sqlConnection);
        }

        public async Task<List<SexualityDTO>> GetSexualities()
        {
            var data = await _sexualityRepository.GetSexualities();

            List<SexualityDTO> dataDTO = new List<SexualityDTO>();

            foreach (SqlSexualityData sexuality in data)
            {
                dataDTO.Add(SexualityMapper.ToDto(sexuality));
            }

            return dataDTO;
        }
    }
}
