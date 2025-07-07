using System.Data.Common;
using Dapper;
using MatchService.Data.DTO;
using Microsoft.Data.SqlClient;

namespace MatchService.Repository
{
    public class MatchRepository : IMatchRepository
    {
        private SqlConnection _connection;


        public MatchRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;
        }

        public async Task<bool> VerifyIfIsMatch(SendLikeDTO likeDTO)
        {
            try
            {
                const string query = "SELECT * FROM MATCH WHERE USUARIOREMETENTE = @IdDestinatario AND USUARIODESTINATARIO = @IdRemetente AND USUARIOREMETENTEACAO = 1";

                var count = await _connection.ExecuteScalarAsync<int>(query, new { IdDestinatario = likeDTO.RecieverId, IdRemetente = likeDTO.SenderId });

                return count > 0;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }

        }

        public async Task<bool> VerifyIfRegisterExist(SendLikeDTO likeDTO)
        {
            try
            {
                const string query = "SELECT * FROM MATCH WHERE USUARIOREMETENTE = @IdRemetente AND USUARIODESTINATARIO = @IdDestinatario AND USUARIODESTINATARIOACAO IS NULL";

                var count = await _connection.ExecuteScalarAsync<int>(query, new { IdDestinatario = likeDTO.RecieverId, IdRemetente = likeDTO.SenderId });

                return count > 0;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }

        }

        public async Task InsertUserLike(SendLikeDTO likeDTO)
        {
            try
            {
                const string query = @"
                    INSERT INTO MATCH(USUARIOREMETENTE, USUARIODESTINATARIO, USUARIOREMETENTEACAO, MATCHACAOREMETENTECREATEDAT)
                    VALUES(@IdRemetente, @IdDestinatario, 1, @DataAtual)   
                ";

                var userData = await _connection.ExecuteAsync(query,
                    new
                    {
                        IdRemetente = likeDTO.SenderId,
                        IdDestinatario = likeDTO.RecieverId,
                        DataAtual = DateTime.Now,
                    }
                    );
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }

        }

        public async Task UpdateUserLikeByReciever(SendLikeDTO likeDTO)
        {
            try
            {
                const string query = @"
                    UPDATE MATCH SET USUARIODESTINATARIOACAO = 1, DATAMATCH = @DataAtual, MATCHACAODESTINATARIOCREATEDAT = @DataAtual WHERE USUARIOREMETENTE = @IdDestinatario AND USUARIODESTINATARIO = @IdRemetente
                ";

                var userData = await _connection.ExecuteAsync(query,
                    new
                    {
                        IdRemetente = likeDTO.SenderId,
                        IdDestinatario = likeDTO.RecieverId,
                        DataAtual = DateTime.Now,
                    }
                    );
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }

        }

        public async Task UpdateUserLikeBySender(SendLikeDTO likeDTO)
        {
            try
            {
                const string query = @"
                    UPDATE MATCH SET MATCHACAOREMETENTECREATEDAT = @DataAtual WHERE USUARIOREMETENTE = @IdRemetente AND USUARIODESTINATARIO = @IdDestinatario
                ";

                var userData = await _connection.ExecuteAsync(query,
                    new
                    {
                        IdRemetente = likeDTO.SenderId,
                        IdDestinatario = likeDTO.RecieverId,
                        DataAtual = DateTime.Now,
                    }
                    );
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }

        }

        public async Task<bool> DoUsersExist(SendLikeDTO likeDTO)
        {
            const string query = @"
            SELECT COUNT(1)
            FROM USUARIOS
            WHERE IDUSUARIO IN (@SenderId, @RecieverId)";

            var count = await _connection.ExecuteScalarAsync<int>(query, new
            {
                SenderId = likeDTO.SenderId,
                RecieverId = likeDTO.RecieverId
            });

            return count == 2;
        }



    }
}
