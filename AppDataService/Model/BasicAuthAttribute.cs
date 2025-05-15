using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;

public class BasicAuthAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string _username;
    private readonly string _password;

    public BasicAuthAttribute(IOptions<BasicAuthConfig> options)
    {
        _username = options.Value.Username;
        _password = options.Value.Password;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (authHeader == null || !AuthenticationHeaderValue.TryParse(authHeader, out var headerValue))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (headerValue.Scheme != "Basic")
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var credentialBytes = Convert.FromBase64String(headerValue.Parameter ?? string.Empty);
        var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

        if (credentials.Length != 2 || credentials[0] != _username || credentials[1] != _password)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
