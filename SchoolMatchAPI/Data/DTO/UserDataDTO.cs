using AccountService.Model.MongoModels;

namespace AccountService.Data.DTO
{
    public class UserDataDTO
    {
        // Campos do MongoUserDataDTO
        public string? MongoId { get; set; }
        public int? IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Curso { get; set; }
        public string BlocoPrincipal { get; set; }
        public List<string> BlocosSecundarios { get; set; }
        public string Sexualidade { get; set; }
        public string Genero { get; set; }
        public string Bio { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cidade { get; set; }
        public string Signo { get; set; }
        public string Semestre { get; set; }
        public string LinguagemAmor { get; set; }
        public List<string> Interesses { get; set; }
        public string Pets { get; set; }
        public string Bebida { get; set; }
        public string Fuma { get; set; }
        public string AtividadeFisica { get; set; }
        public string TipoRole { get; set; }
        public bool ExibirSexualidade { get; set; }
        public SpotifyMusicModel SpotifyMusicData { get; set; }

        // Campos do SQLUserDataDTO
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public bool UsuarioVerificado { get; set; }
        public int CursoId { get; set; }
        public int GeneroId { get; set; }
        public List<int> UsuarioPreferencia { get; set; }
        public DateTime UsuarioCreatedAt { get; set; }
        public DateTime UsuarioEditedAt { get; set; }
        public int BlocoPrincipalId { get; set; }
        public List<int> BlocosUsuario { get; set; }
    }
}
