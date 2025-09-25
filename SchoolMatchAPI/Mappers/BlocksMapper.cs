using AccountService.Data.DTO;
using AccountService.Model.SqlModels;

namespace AccountService.Mappers
{
    public class BlocksMapper
    {
        internal static BlocksDTO ToDto(SqlBlocks sqlData)
        {
            var model = new BlocksDTO
            {
                // SQL fields
                BlockId = sqlData.IdBloco,
                BlockName = sqlData.NomeBloco,

            };

            return model;
        }

        //internal static SqlUserData ToSqlModel(this UserDataDTO dto)
        //{
        //    var model = new SqlUserData
        //    {
        //        // SQL DTO fields
        //        NomeUsuario = dto.Nome,
        //        SobrenomeUsuario = dto.Sobrenome,
        //        EmailUsuario = dto.EmailUsuario,
        //        SenhaUsuario = dto.SenhaUsuario,
        //        UsuarioVerificado = dto.UsuarioVerificado,
        //        CursoUsuario = dto.Curso.CourseId,
        //        UsuarioGenero = dto.Genero.GenderId,
        //        UsuarioPreferenciaGenero = dto.UsuarioPreferencia,
        //        UsuarioCreatedAt = dto.UsuarioCreatedAt,
        //        UsuarioEditedAt = dto.UsuarioEditedAt,
        //        BlocosUsario = dto.BlocosUsuario,
        //        BlocoPrincipal = dto.BlocoPrincipal
        //    };

        //    return model;
        //}
    }
}
