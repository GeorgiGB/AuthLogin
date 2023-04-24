using AuthLogin.Data;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace AuthLogin.Components
{
    public partial class BlocNota
    {
        public int Id { get; set; }
        [Parameter] public string Titulo { get; set; }

        [Parameter] public string Descripcion { get; set; }

        [Parameter] public EventCallback OnClick { get; set; }

        private readonly List<string> colores = new() { "#F94144", "#F3722C", "#F8961E", "#F9C74F", "#90BE6D", "#43AA8B", "#577590" };
    }

}
