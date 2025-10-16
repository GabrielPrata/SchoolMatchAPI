using MatchService.Data.DTO.Profile;
using MatchService.Model.Base;

namespace MatchService.Model.SqlModels
{
    public class SqlUserData
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string SobrenomeUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public int CursoUsuario { get; set; }
        public int UsuarioGenero { get; set; }
    }
}
