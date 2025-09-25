namespace AccountService.Data.DTO
{
    public class GenderDTO
    {
        public GenderDTO(int genderId, string genderName)
        {
            GenderId = genderId;
            GenderName = genderName;
        }

        public GenderDTO() { }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }
}
