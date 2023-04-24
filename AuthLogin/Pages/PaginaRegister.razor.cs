using AuthLogin.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace AuthLogin.Pages
{
    public partial class PaginaRegister
    {
        private Usuario user = new();
        //private DbConnection _dbContext = new DbConnection();
        private string errorMsg = "";
        private bool showError = false;


        [Inject] UserService _userService { get; set; }

        [Inject] NavigationManager navManager { get; set; }

        [Inject] DbConnection _dbContext { get; set; }

        public async Task RegisterUser()
        {
            // Comprueba primero si existe el nombre en la base de datos
            var usuarioExiste = await _userService.LookupUserInDatabase(user.Username);
            
            // Si existe manda error
            if (usuarioExiste !=null)
            {
                showError = true;
                errorMsg = "No se pudo registrar el usuario. Por favor, inténtalo de nuevo.";
                
            }
                user.Password = DbConnection.HashPassword(user.Password);
                _dbContext.Usuarios.Add(user);
                await _dbContext.SaveChangesAsync();
                navManager.NavigateTo("/login");  
            
        }

    }
}
