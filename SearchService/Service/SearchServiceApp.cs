using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SearchService.Data.DTO;
using SearchService.Data.DTO.Profile;
using SearchService.Mappers;
using SearchService.Model.Base;
using SearchService.Model.MongoModels;
using SearchService.Model.SqlModels;
using SearchService.Repository;

namespace SearchService.Service
{
    //Adicionei esse App ao fim para nao conflitar com o nome da solução
    public class SearchServiceApp : ISearchService
    {
        private readonly UserDataSqlRepository _sqlRepository;
        private readonly UserDataMongoRepository _mongoRepository;

        public SearchServiceApp(string sqlConnection, string mongoConnection)
        {

            _sqlRepository = new UserDataSqlRepository(sqlConnection);
            _mongoRepository = new UserDataMongoRepository(mongoConnection);
        }

        public async Task<List<UserDataDTO>> DefaultSearch(UserPreferencesDTO dto)
        {
            IEnumerable<SqlUserData> sqlData = await _sqlRepository.DefaultSearch(dto);

            if (sqlData.Count() == 0)
            {
                throw new ApiException(new ApiErrorModel("Nenhum Usuário encontrado!", 404));
            }

            List<UserDataDTO> usersData = new List<UserDataDTO>();

            foreach (SqlUserData sqlUserData in sqlData)
            {
                IEnumerable<SqlBlocks> userBlocks = await _sqlRepository.GetUserBlocksById(sqlUserData.IdUsuario);
                IEnumerable<SqlPreferences> userPreferences = await _sqlRepository.GetUserPreferencesById(sqlUserData.IdUsuario);

                sqlUserData.BlocosUsario = userBlocks.Where(b => b.BlocoPrincipal == false).Select(b => BlocksMapper.ToDto(b)).ToList();
                sqlUserData.BlocoPrincipal = userBlocks.Where(b => b.BlocoPrincipal == true).Select(b => BlocksMapper.ToDto(b)).FirstOrDefault();
                sqlUserData.UsuarioPreferenciaGenero = userPreferences.Select(PreferencesMapper.ToDto).ToList();
                MongoUserData mongoData = await _mongoRepository.GetUserById(sqlUserData.IdUsuario);

                usersData.Add(UserMapper.ToDto(sqlUserData, mongoData));
            }

            return usersData;

        }


        //Melhorar as mensagens de retorno
        //public async Task<LikeResponseDTO> SendUserLike(SendLikeDTO likeDTO)
        //{

        //    if (!await _matchRepository.DoUsersExist(likeDTO))
        //    {
        //        throw new ApiException(new ApiErrorModel("Usuário não encontrado em nossa base de dados!", 404));
        //    }


        //    if (await _matchRepository.VerifyIfIsMatch(likeDTO))
        //    {
        //        await _matchRepository.UpdateUserLikeByReciever(likeDTO);
        //        return new LikeResponseDTO
        //        {
        //            IsMatch = true,
        //            Message = "Novo Match!"
        //        };

        //    }

        //    if (await _matchRepository.VerifyIfRegisterExist(likeDTO))
        //    {
        //        await _matchRepository.UpdateUserLikeBySender(likeDTO);
        //    }
        //    else
        //    {
        //        await _matchRepository.InsertUserLike(likeDTO);
        //    }


        //    return new LikeResponseDTO
        //    {
        //        IsMatch = false,
        //        Message = "Curtida enviada com sucesso."
        //    };
        //}
    }
}
