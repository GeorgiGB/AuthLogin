using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthLogin.Data
{
    public class AuthStateProvider : AuthenticationStateProvider, IDisposable
    {
        private readonly UserService _userService;//conexion con la base de datos
        private readonly Usuario usuarios;//clase usuario

        public AuthStateProvider(UserService userService)
        {
            _userService = userService;
            AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        }

        public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;


        public async Task LoginAsync(string username)
        {
            var principal = new ClaimsPrincipal();
            var userLogin = _userService.LookupUserInDatabase(username);

            if (userLogin is not null)
            {
                principal = usuarios.ToClaimsPrincipal();
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public async Task LogoutAsync()
        {
            await _userService.ClearBrowserUserDataAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();
            var user = await _userService.FetchUserFromBrowserAsync();

            if (user is not null)
            {
                var userInDatabase = _userService.LookupUserInDatabase(user.Username);

                if (userInDatabase is not null)
                {
                    principal = usuarios.ToClaimsPrincipal();
                    CurrentUser = usuarios;
                }
            }

            return new(principal);
        }

        public Usuario CurrentUser { get; private set; } = new();

        private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
        {
            var authenticationState = await task;

            if (authenticationState is not null)
            {
                CurrentUser = Usuario.FromClaimsPrincipals(authenticationState.User);
            }
        }

    }
}

