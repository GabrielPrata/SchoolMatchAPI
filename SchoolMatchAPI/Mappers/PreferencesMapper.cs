using AccountService.Data.DTO;
using AccountService.Model.SqlModels;

namespace AccountService.Mappers
{
    public class PreferencesMapper
    {
        internal static GenderDTO ToDto(SqlPreferences sqlData)
        {
            var model = new GenderDTO
            {
                // SQL fields
                GenderId = sqlData.GenderId,
                GenderName = sqlData.GenderName,

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
