using MatchService.Data.DTO;

namespace MatchService.Service
{
    public interface IMatchService
    {
        Task<LikeResponseDTO> SendUserLike(SendLikeDTO likeDTO);
    }
}
