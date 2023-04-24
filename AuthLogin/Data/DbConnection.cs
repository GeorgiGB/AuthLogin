using AuthLogin.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AuthLogin.Data
{
    public class DbConnection : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Nota> Notas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=<~\\Downloads\\SQLiteStudio\\dbnotas2.db>;");
            // COMO ENLAZAR LA BASE DE DATOS PARA CONECTAR DESDE SQ SERVER STUDIO
        }

        public async Task CrearNotaAsync(string titulo, string contenido, int usuarioId)
        {
            var nota = new Nota
            {
                Titulo = titulo,
                Contenido = contenido,
                UsuarioId = usuarioId
            };

            Notas.Add(nota);
            await SaveChangesAsync();
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedString = Convert.ToBase64String(hashedBytes);
            return hashedString;
        }

    }
}
