using AuthLogin.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace AuthLogin.Pages
{
    public partial class PaginaLogin
    {
        private PaginaLogin paginaLogin = new();
        [Parameter] public string Username { get;set; }

        [Parameter] public string Password { get;set; }

        [Parameter] public string ErrorMessage { get;set; }

        [Inject] NavigationManager NavigationManager { get; set; }

        [Inject] UserService userService { get; set; }

        private bool? userExists;

        private async Task HandleValidSubmit()
        {
            var user = userService.LookupUserInDatabase(Username);

            if (user != null)
            {
                
                NavigationManager.NavigateTo("/authentication/notas");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }
    }
}
