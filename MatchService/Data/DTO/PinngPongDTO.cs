namespace MatchService.Data.DTO
{
    public class PinngPongDTO
    {

        public PinngPongDTO(string response)
        {
            Reponse = response;
        }

        public string Reponse { get; set; }
    }
}
