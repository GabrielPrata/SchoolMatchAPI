using Microsoft.AspNetCore.Identity;

namespace IdentityService.Model
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId {  get; set; }
        public string Nome {  get; set; }
        public string Sobrenome { get; set; }
        public string Email {  get; set; }

    }
}
