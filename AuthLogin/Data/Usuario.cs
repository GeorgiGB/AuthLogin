using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthLogin.Data
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Roles { get; set; } = "admin";

        public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
        {
            new (ClaimTypes.Name, Username),
            new (ClaimTypes.Hash, Password),
            new (ClaimTypes.Role, Roles)
        }));

        public static Usuario FromClaimsPrincipals(ClaimsPrincipal principal) => new()
        {
            Username = principal.FindFirstValue(ClaimTypes.Name),
            Password = principal.FindFirstValue(ClaimTypes.Hash)
        };
    }
}
