using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using System.Security.Claims;

public class CustomProfileService : IProfileService
{
    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        // Retorna todos os claims do usuário (já definidos no ExternalGrantValidator)
        context.IssuedClaims = context.Subject.Claims.ToList();
        return Task.CompletedTask;
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        // Sempre considera o usuário como ativo
        context.IsActive = true;
        return Task.CompletedTask;
    }
}
