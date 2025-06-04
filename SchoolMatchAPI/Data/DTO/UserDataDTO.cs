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
        public SexualityDTO Sexualidade { get; set; }
        public string Bio { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cidade { get; set; }
        public CourseDurationDTO Semestre { get; set; }
        public UserAboutDTO UserAbout { get; set; }
        public List<InterestsDTO> Interesses { get; set; }
        public SpotifyMusicModel SpotifyMusicData { get; set; }

        // Campos do SQLUserDataDTO
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public bool UsuarioVerificado { get; set; }
        public CourseDTO Curso { get; set; }
        public GenderDTO Genero { get; set; }
        public List<GenderDTO> UsuarioPreferencia { get; set; }
        public DateTime UsuarioCreatedAt { get; set; }
        public DateTime UsuarioEditedAt { get; set; }
        public BlocksDTO BlocoPrincipal { get; set; }
        public List<BlocksDTO> BlocosUsuario { get; set; }
    }
}
