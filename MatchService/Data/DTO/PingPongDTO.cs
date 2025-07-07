namespace MatchService.Data.DTO
{
    public class PingPongDTO
    {

        public PingPongDTO(string response)
        {
            Reponse = response;
        }

        public string Reponse { get; set; }
    }
}
