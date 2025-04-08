using AppDataService.Data.DTO;

namespace AppDataService.Service
{
    public interface ISexualityDataService
    {
        Task<List<SexualityDTO>> GetSexualities();
    }
}
