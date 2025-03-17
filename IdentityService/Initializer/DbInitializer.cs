using IdentityModel;
using IdentityService.Model;
using IdentityService.Model.Context;
using IdentityService.Models.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityService.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly SQLServerContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(SQLServerContext context, UserManager<ApplicationUser> user, RoleManager<IdentityRole> role)
        {
            _context = context;
            _user = user;
            _role = role;
        }

        public void Initialize()
        {
            if (_role.FindByNameAsync(IdentityConfiguration.Client).Result != null) return;
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client))
                .GetAwaiter()
                .GetResult();

            ApplicationUser user = new ApplicationUser()
            {
                UserName = "GabrielPrata",
                Nome = "Gabriel",
                Sobrenome = "Prata",
                Email = "prata@alunos.fho.edu.br",
                UserId = 25,
                EmailConfirmed = true,
            };

            _user.CreateAsync(user, "Senha123!")
                .GetAwaiter()
                .GetResult();

            _user.AddToRoleAsync(user, IdentityConfiguration.Client)
                .GetAwaiter()
                .GetResult();

            var userClaims = _user.AddClaimsAsync(user, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, user.UserName),
                new Claim(JwtClaimTypes.GivenName, user.Nome),
                new Claim(JwtClaimTypes.FamilyName, user.Sobrenome),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),

            }).Result;
        }
    }
}
