using MatchService.Data.DTO;
using MatchService.Model.Base;
using MatchService.Repository;
using Microsoft.Data.SqlClient;

namespace MatchService.Service
{
    //Adicionei esse App ao fim para nao conflitar com o nome da solução
    public class MatchServiceApp : IMatchService
    {
        private readonly MatchRepository _matchRepository;

        public MatchServiceApp(SqlConnection sqlConnection)
        {
            _matchRepository = new MatchRepository(sqlConnection);
        }


        //Melhorar as mensagens de retorno
        public async Task<LikeResponseDTO> SendUserLike(SendLikeDTO likeDTO)
        {

            if (!await _matchRepository.DoUsersExist(likeDTO))
            {
                throw new ApiException(new ApiErrorModel("Usuário não encontrado em nossa base de dados!", 404));
            }


            if (await _matchRepository.VerifyIfIsMatch(likeDTO))
            {
                await _matchRepository.UpdateUserLikeByReciever(likeDTO);
                return new LikeResponseDTO
                {
                    IsMatch = true,
                    Message = "Novo Match!"
                };

            }

            if (await _matchRepository.VerifyIfRegisterExist(likeDTO))
            {
                await _matchRepository.UpdateUserLikeBySender(likeDTO);
            }
            else
            {
                await _matchRepository.InsertUserLike(likeDTO);
            }


            return new LikeResponseDTO
            {
                IsMatch = false,
                Message = "Curtida enviada com sucesso."
            };
        }
    }
}
