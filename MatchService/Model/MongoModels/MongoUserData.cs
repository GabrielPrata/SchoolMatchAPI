using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MatchService.Data.DTO.Profile;

namespace MatchService.Model.MongoModels
{
    public class MongoUserData
    {
        [BsonId]  // Indica que este campo é o identificador único do documento
        [BsonRepresentation(BsonType.ObjectId)]  // Garante que o _id seja tratado como ObjectId pelo MongoDB
        public string _id { get; set; }

        [BsonElement("IdUsuario")]
        public int IdUsuario { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; }

        [BsonElement("Sobrenome")]
        public string Sobrenome { get; set; }

        [BsonElement("Curso")]
        public CourseDTO Curso { get; set; }

        [BsonElement("BlocoPrincipal")]
        public BlocksDTO BlocoPrincipal { get; set; }

        [BsonElement("BlocosSecundarios")]
        public List<BlocksDTO> BlocosSecundarios { get; set; }

        [BsonElement("Sexualidade")]
        public SexualityDTO Sexualidade { get; set; }

        [BsonElement("Genero")]
        public GenderDTO Genero { get; set; }

        [BsonElement("Bio")]
        public string Bio { get; set; }

        [BsonElement("DataNascimento")]
        public DateTime DataNascimento { get; set; }

        [BsonElement("Cidade")]
        public string Cidade { get; set; }

        [BsonElement("Semestre")]
        public CourseDurationDTO Semestre { get; set; }

        [BsonElement("Interesses")]
        public List<InterestsDTO> Interesses { get; set; }

        [BsonElement("SobreUsuario")]
        public UserAboutDTO SobreUsuario { get; set; }

        [BsonElement("CriadoEm")]
        public DateTime CriadoEm { get; set; }
        [BsonElement("ModificadoEm")]
        public DateTime ModificadoEm { get; set; }

        [BsonElement("SpotifyMusicData")]
        public SpotifyMusicModel SpotifyMusicData { get; set; }

        [BsonElement("UserBase64Images")]
        public List<string> UserBase64Images { get; set; }
    }
}

