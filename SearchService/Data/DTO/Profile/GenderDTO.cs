namespace SearchService.Data.DTO.Profile
{
    public class GenderDTO
    {
        public GenderDTO(int genderId, string genderName)
        {
            GenderId = genderId;
            GenderName = genderName;
        }

        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }
}
