using AppDataService.Data.DTO;
using AppDataService.Mappers;
using AppDataService.Model.SqlModels;
using AppDataService.Repository;
using Microsoft.Data.SqlClient;

namespace AppDataService.Service
{
    public class InterestsDataService : IInterestsDataService
    {
        private readonly InterestsDataRepository _interestsRepository;

        public InterestsDataService(SqlConnection sqlConnection)
        {
            _interestsRepository = new InterestsDataRepository(sqlConnection);
        }

        public async Task<List<InterestsDTO>> GetAllInterests()
        {
            var data = await _interestsRepository.GetAllInterests();

            List<InterestsDTO> dataDTO = new List<InterestsDTO>();

            foreach (SqlInterestsData interest in data)
            {
                dataDTO.Add(InterestsMapper.ToDto(interest));
            }

            return dataDTO;
        }
    }
}
