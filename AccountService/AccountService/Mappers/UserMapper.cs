using AccountService.Data.DTO;
using AccountService.Model;

namespace AccountService.Mappers
{
    internal static class UserMapper
    {
        internal static UserDataDTO ToDto(SqlUserData sqlData, MongoUserData mongoData)
        {
            var model = new UserDataDTO
            {
                // SQL fields

                IdUsuario = sqlData.IdUsuario,
                Nome = sqlData.NomeUsuario,
                Sobrenome = sqlData.SobrenomeUsuario,
                EmailUsuario = sqlData.EmailUsuario,
                SenhaUsuario = sqlData.SenhaUsuario,
                UsuarioVerificado = sqlData.UsuarioVerificado,
                CursoId = sqlData.CursoUsuario,
                GeneroId = sqlData.UsuarioGenero,
                UsuarioPreferencia = sqlData.UsuarioPreferenciaGenero,

                UsuarioCreatedAt = sqlData.UsuarioCreatedAt,
                UsuarioEditedAt = sqlData.UsuarioEditedAt,

                // Mongo fields
                MongoId = mongoData._id,
                curso = mongoData.Curso,
                BlocoPrincipal = mongoData.BlocoPrincipal,
                BlocosSecundarios = mongoData.BlocosSecundarios,
                Sexualidade = mongoData.Sexualidade,
                Genero = mongoData.Genero,
                Bio = mongoData.Bio,
                DataNascimento = mongoData.DataNascimento,
                Cidade = mongoData.Cidade,
                Signo = mongoData.Signo,
                Semestre = mongoData.Semestre,
                LinguagemAmor = mongoData.LinguagemAmor,
                Interesses = mongoData.Interesses,
                Pets = mongoData.Pets,
                Bebida = mongoData.Bebida,
                Fuma = mongoData.Fuma,
                AtividadeFisica = mongoData.AtividadeFisica,
                TipoRole = mongoData.TipoRole,
                UrlMusica = mongoData.UrlMusica,
                ExibirSexualidade = mongoData.ExibirSexualidade,
            };

            return model;
        }

        internal static SqlUserData ToSqlModel(this UserDataDTO dto)
        {
            var model = new SqlUserData
            {
                // SQL DTO fields
                IdUsuario = dto.IdUsuario,
                NomeUsuario = dto.Nome,
                SobrenomeUsuario = dto.Sobrenome,
                EmailUsuario = dto.EmailUsuario,
                SenhaUsuario = dto.SenhaUsuario,
                UsuarioVerificado = dto.UsuarioVerificado,
                CursoUsuario = dto.CursoId,
                UsuarioGenero = dto.GeneroId,
                UsuarioPreferenciaGenero = dto.UsuarioPreferencia,
                UsuarioCreatedAt = dto.UsuarioCreatedAt,
                UsuarioEditedAt = dto.UsuarioEditedAt,
            };

            return model;
        }

        internal static MongoUserData ToMongoModel(this UserDataDTO dto)
        {
            var model = new MongoUserData
            {
                // Mongo DTO fields
                Curso = dto.curso,
                BlocoPrincipal = dto.BlocoPrincipal,
                BlocosSecundarios = dto.BlocosSecundarios,
                Sexualidade = dto.Sexualidade,
                Genero = dto.Genero,
                Bio = dto.Bio,
                DataNascimento = dto.DataNascimento,
                Cidade = dto.Cidade,
                Signo = dto.Signo,
                Semestre = dto.Semestre,
                LinguagemAmor = dto.LinguagemAmor,
                Interesses = dto.Interesses,
                Pets = dto.Pets,
                Bebida = dto.Bebida,
                Fuma = dto.Fuma,
                AtividadeFisica = dto.AtividadeFisica,
                TipoRole = dto.TipoRole,
                UrlMusica = dto.UrlMusica,
                ExibirSexualidade = dto.ExibirSexualidade,
                CriadoEm = dto.UsuarioCreatedAt,
                ModificadoEm = dto.UsuarioEditedAt,
            };

            return model;
        }
    }
}
