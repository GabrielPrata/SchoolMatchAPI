using MatchService.Data.DTO;
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


        public async Task<LikeResponseDTO> SendUserLike(SendLikeDTO likeDTO)
        {

            //preciso verificar se o remetente ja me curtiu
            
            if (await _matchRepository.VerifyIfIsMatch(likeDTO))
            {
                await _matchRepository.UpdateUserLike(likeDTO);
                return new LikeResponseDTO
                {
                    IsMatch = true,
                    Message = "Parabéns! É um match 🎉"
                };

            }
            else
            {
                //Preciso ver se o registro já nao existe, para evitar registros duplicados
                await _matchRepository.InsertUserLike(likeDTO);

                return new LikeResponseDTO
                {
                    IsMatch = false,
                    Message = "Curtida enviada com sucesso."
                };
            }


        }
    }
}
