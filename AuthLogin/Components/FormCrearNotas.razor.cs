using AuthLogin.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AuthLogin.Components
{
    public partial class FormCrearNotas
    {
        [Parameter] public string CancelText { get; set; } = "Cancelar";
        [Parameter] public string OkText { get; set; } = "Crear";


        [Required(ErrorMessage = "Falta un titulo")]
        public string NuevoTitulo { get; set; }
        [Required(ErrorMessage = "Falta una descripcion")]
        public string NuevaDescripcion { get; set; }

        [Required(ErrorMessage = "Falta un mensaje")]
        public string ErrorMessage { get; set; }

        [Inject] DbConnection dB { get; set; }

        [Inject] UserService _userService { get; set; }

        [Inject] AuthenticationStateProvider AuthStateProvider { get; set; }
        
        public async Task CrearNota()
        {
            try
            {
                var user = (await AuthStateProvider.GetAuthenticationStateAsync()).User;
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                await dB.CrearNotaAsync(NuevoTitulo, NuevaDescripcion, int.Parse(userId));
                NuevoTitulo = "";
                NuevaDescripcion = "";
                ErrorMessage = "";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        
    }
}
