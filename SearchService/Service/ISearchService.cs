//using SearchService.Data.DTO;

using SearchService.Data.DTO;
using SearchService.Data.DTO.Profile;

namespace SearchService.Service
{
    public interface ISearchService
    {
        Task<List<UserDataDTO>> DefaultSearch(UserPreferencesDTO dto);
        Task<List<UserDataDTO>> SearchByCourseAndBlock(UserSearchDTO dto);
    }
}
