using AppDataService.Data.DTO;

namespace AppDataService.Service
{
    public interface IInterestsDataService
    {
        Task<List<InterestsDTO>> GetAllInterests();
    }
}
