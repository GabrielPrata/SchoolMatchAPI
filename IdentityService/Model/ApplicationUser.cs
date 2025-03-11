using Microsoft.AspNetCore.Identity;

namespace IdentityService.Model
{
    public class ApplicationUser : IdentityUser
    {
        private int id {  get; set; }
        private string Nome {  get; set; }
        private string Sobrenome { get; set; } 
        private string Email {  get; set; }

    }
}
