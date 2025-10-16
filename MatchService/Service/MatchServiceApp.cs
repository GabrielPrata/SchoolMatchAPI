using MatchService.Data.DTO;
using MatchService.Data.DTO.Profile;
using MatchService.Model.Base;
using MatchService.Model.MongoModels;
using MatchService.Model.SqlModels;
using MatchService.Repository;
using MatchService.Repository.Mongo;
using Microsoft.Data.SqlClient;
using SearchService.Mappers;

namespace MatchService.Service
{
    //Adicionei esse App ao fim para nao conflitar com o nome da solução
    public class MatchServiceApp : IMatchService
    {
        private readonly MatchRepository _matchRepository;
        private readonly UserDataMongoRepository _mongoRepository;

        public MatchServiceApp(SqlConnection sqlConnection, string mongoConnection)
        {
            _matchRepository = new MatchRepository(sqlConnection);
            _mongoRepository = new UserDataMongoRepository(mongoConnection);
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


        public async Task<List<UserDataDTO>> GetUserMatches(int userId)
        {
            IEnumerable<SqlUserData> userMatchs = await _matchRepository.GetUserMatchs(userId);

            List<UserDataDTO> usersData = new List<UserDataDTO>();

            foreach (SqlUserData match in userMatchs)
            {
                MongoUserData mongoData = await _mongoRepository.GetUserById(match.IdUsuario);

                usersData.Add(UserMapper.ToDto(match, mongoData));
            }

            return usersData;

        }
    }
}
