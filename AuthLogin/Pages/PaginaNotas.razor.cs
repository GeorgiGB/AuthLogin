using AuthLogin.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace AuthLogin.Pages
{
    public partial class PaginaNotas
    {
        [Parameter] public string Username { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                Username = user.Identity.Name;
            }
        }
    }
}
