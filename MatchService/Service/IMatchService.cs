using MatchService.Data.DTO;
using MatchService.Data.DTO.Profile;

namespace MatchService.Service
{
    public interface IMatchService
    {
        Task<LikeResponseDTO> SendUserLike(SendLikeDTO likeDTO);
        Task<List<UserDataDTO>> GetUserMatches(int userId);
    }
}
