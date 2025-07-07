using MatchService.Data.DTO;

namespace MatchService.Repository
{
    public interface IMatchRepository
    {
        Task<bool> VerifyIfIsMatch(SendLikeDTO likeDTO);
        Task InsertUserLike(SendLikeDTO likeDTO);
        Task UpdateUserLike(SendLikeDTO likeDTO);
    }
}
