using AccountService.Data.DTO;
using AccountService.Model.Base;

namespace AccountService.Model.SqlModels
{
    public class SqlUserData : ControleAlteracao
    {
        public int? IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string SobrenomeUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public bool UsuarioVerificado { get; set; }
        public int CursoUsuario { get; set; }
        public int UsuarioGenero { get; set; }
        public List<GenderDTO> UsuarioPreferenciaGenero { get; set; }
        public List<BlocksDTO> BlocosUsario { get; set; }
        public BlocksDTO BlocoPrincipal { get; set; }
    }
}
