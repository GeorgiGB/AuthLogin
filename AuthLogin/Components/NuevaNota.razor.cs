using Microsoft.AspNetCore.Components;

namespace AuthLogin.Components
{
    public partial class NuevaNota
    {
        [Parameter] public string NuevoTitulo { get; set; }

        [Parameter] public string NuevaDescripcion { get; set; }

        public bool MostrarFormulario { get; set; }

        [Parameter] public EventCallback OnCancel { get; set; }

        [Parameter] public Action OnClick { get; set; }
        [Parameter] public string? NombreBoton { get; set; }
    }
}
