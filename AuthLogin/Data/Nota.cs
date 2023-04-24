namespace AuthLogin.Data
{
    public class Nota
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}