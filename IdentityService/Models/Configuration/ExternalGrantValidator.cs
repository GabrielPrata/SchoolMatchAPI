using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using System.Security.Claims;

public class ExternalGrantValidator : IExtensionGrantValidator
{
    public string GrantType => "external";

    public async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
        var userId = context.Request.Raw.Get("user_id");
        var email = context.Request.Raw.Get("email");
        var name = context.Request.Raw.Get("name");

        if (string.IsNullOrWhiteSpace(userId))
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Missing user_id");
            return;
        }

        var claims = new List<Claim>
        {
            new Claim("sub", userId),
            new Claim("email", email ?? ""),
            new Claim("name", name ?? "")
        };

        context.Result = new GrantValidationResult(
            subject: userId,
            authenticationMethod: GrantType,
            claims: claims);
    }
}
