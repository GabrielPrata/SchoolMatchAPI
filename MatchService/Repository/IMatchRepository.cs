using MatchService.Data.DTO;
using MatchService.Model.SqlModels;

namespace MatchService.Repository
{
    public interface IMatchRepository
    {
        Task<bool> VerifyIfIsMatch(SendLikeDTO likeDTO);
        Task InsertUserLike(SendLikeDTO likeDTO);
        Task UpdateUserLikeByReciever(SendLikeDTO likeDTO);
        Task UpdateUserLikeBySender(SendLikeDTO likeDTO);
        Task<bool> VerifyIfRegisterExist(SendLikeDTO likeDTO);
        Task<bool> DoUsersExist(SendLikeDTO likeDTO);
        Task<IEnumerable<SqlUserData>> GetUserMatchs(int senderUserId);
    }
}
