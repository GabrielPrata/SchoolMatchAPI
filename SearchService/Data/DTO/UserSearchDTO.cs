
using SearchService.Data.DTO.Profile;

namespace SearchService.Data.DTO
{
    public class UserSearchDTO
    {
        public UserPreferencesDTO Preferences { get; set; }
        public int? CourseId { get; set; }
        public int? BlockId { get; set; }
    }
}
