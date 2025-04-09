using AccountService.Model.SqlModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AccountService.Repository.Queries
{
    public class UserDataSqlQueries
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public UserDataSqlQueries(string strConnection)
        {
            _connectionString = strConnection;
        }

        private SqlConnection GetOpenConnection()
        {
            if (_connection == null || string.IsNullOrEmpty(_connection.ConnectionString))
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
                return _connection;
            }
            return _connection;
        }


        public async Task<SqlUserData?> GetUserDataById(int userId)
        {
            const string query = "SELECT * FROM USUARIOS WHERE IDUSUARIO= @id";

            await using var conn = GetOpenConnection();
            var userData = await conn.QuerySingleOrDefaultAsync<SqlUserData?>(query, new { id = userId });
            return userData;
        }

        public async Task<int> SaveUserData(SqlUserData data)
        {
            const string query = @"
        INSERT INTO USUARIOS (NOMEUSUARIO, SOBRENOMEUSUARIO, EMAILUSUARIO, SENHAUSUARIO, USUARIOVERIFICADO, CURSOUSUARIO, USUARIOGENERO, USUARIOPREFERENCIAGENERO, USUARIOCREATEDAT)
        VALUES (@Nome, @Sobrenome, @Email, @Senha, @Verificado, @Curso, @Genero, @PreferenciaGenero, @CreatedAt);
        SELECT CAST(SCOPE_IDENTITY() as int);
    ";

            await using var conn = GetOpenConnection();
            var userId = await conn.ExecuteScalarAsync<int>(query, new
            {
                Nome = data.NomeUsuario,
                Sobrenome = data.SobrenomeUsuario,
                Email = data.EmailUsuario,
                Senha = data.SenhaUsuario,
                Verificado = data.UsuarioVerificado,
                Curso = data.CursoUsuario,
                Genero = data.UsuarioGenero,
                PreferenciaGenero = data.UsuarioPreferenciaGenero,
                CreatedAt = data.UsuarioCreatedAt,
            });

            return userId;
        }


        public async Task SaveUserGenreInterests(int userId, int genreId)
        {
            const string query = @"
                INSERT INTO USUARIOINTERESSEGENERO(IDUSUARIO, IDGENERO)
                VALUES(@IdUsuario, @IdGenero)   
            ";

            await using var conn = GetOpenConnection();
            var userData = await conn.ExecuteAsync(query,
                new
                {
                    IdUsuario = userId,
                    IdGenero = genreId,
                }
                );
        }

        public async Task<bool> VerifyUserExist(string userEmail)
        {
            const string query = @"
                SELECT COUNT(*) FROM USUARIOS WHERE EMAILUSUARIO = @Email;  
            ";

            await using var conn = GetOpenConnection();
            var count = await conn.ExecuteScalarAsync<int>(query, new { Email = userEmail });

            return count > 0;
        }

        public async Task SaveUserBlocks(int userId, int userBlock)
        {
            const string query = @"
                INSERT INTO BLOCOSUSUARIO(IDUSUARIO, IDBLOCO)
                VALUES(@IdUsuario, @IdBloco)   
            ";

            await using var conn = GetOpenConnection();
            var userData = await conn.ExecuteAsync(query,
                new
                {
                    IdUsuario = userId,
                    IdBloco = userBlock,
                }
                );
        }

        public async Task SaveEmailToVerify(string userEmail)
        {
            const string query = @"
                INSERT INTO EMAILVALIDACAO(EMAIL)
                VALUES(@Email)   
            ";
            // TODO: aplicar singleton
            await using var conn = GetOpenConnection();
            var userData = await conn.ExecuteAsync(query,
                new
                {
                    Email = userEmail,
                }
                );
        }

        public async Task<bool> CheckIfEmailVerifyIsPendent(string userEmail)
        {
            const string query = @"
                SELECT COUNT(*) FROM EMAILVALIDACAO WHERE EMAIL = @Email;  
            ";

            await using var conn = GetOpenConnection();
            var count = await conn.ExecuteScalarAsync<int>(query, new { Email = userEmail });

            return count == 0;
        }

        public async Task<bool> CheckIfEmailIsVerified(string userEmail)
        {
            const string query = @"
                SELECT COUNT(*) FROM EMAILVALIDACAO WHERE EMAIL = @Email AND STATUS = 1;  
            ";

            await using var conn = GetOpenConnection();
            var count = await conn.ExecuteScalarAsync<int>(query, new { Email = userEmail });

            return count > 0;
        }

    }
}
