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
        public List<int> UsuarioPreferenciaGenero { get; set; }
        public List<int> BlocosUsario { get; set; }
        public int BlocoPrincipalId { get; set; }
    }
}
