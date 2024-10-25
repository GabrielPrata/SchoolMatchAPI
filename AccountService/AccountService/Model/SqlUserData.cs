using AccountService.Model.Base;

namespace AccountService.Model
{
    public class SqlUserData : ControleAlteracao
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string SobrenomeUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public bool UsuarioVerificado { get; set; }
        public int CursoUsuario { get; set; }
        public int UsuarioGenero { get; set; }
        public int UsuarioPreferenciaGenero { get; set; }
    }
}
