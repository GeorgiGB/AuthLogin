using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace AuthLogin.Data
{
    public class UserService
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;

        private readonly DbConnection _dbContext;

        public UserService(ProtectedLocalStorage protectedLocalStorage)
        {
            _protectedLocalStorage = protectedLocalStorage;
        }

        public async Task<Usuario?> LookupUserInDatabase(string username)
        {
            var foundUser = await _dbContext.Usuarios
                .Where(u => u.Username == username)
                .SingleOrDefaultAsync();

            return foundUser;
        }

        private readonly string _blazorSchoolStorageKey = "blazorSchoolIdentity";

        public async Task PersistUserToBrowserAsync(Usuario user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            await _protectedLocalStorage.SetAsync(_blazorSchoolStorageKey, userJson);
        }

        public async Task<Usuario?> FetchUserFromBrowserAsync()
        {
            try
            {
                var storedUserResult = await _protectedLocalStorage.GetAsync<string>(_blazorSchoolStorageKey);

                if (storedUserResult.Success && !string.IsNullOrEmpty(storedUserResult.Value))
                {
                    var user = JsonConvert.DeserializeObject<Usuario>(storedUserResult.Value);

                    return user;
                }
            }
            catch (InvalidOperationException)
            {
            }

            return null;
        }

        public async Task ClearBrowserUserDataAsync() => await _protectedLocalStorage.DeleteAsync(_blazorSchoolStorageKey);

        public async Task RegisterUserAsync(Usuario user)
        {
            var nuevoUsuario = new Usuario()
            {
                  Username = user.Username,
                  Password = user.Password,
                  Roles = "admin",
            };
        }

        public async Task<string> GetUserIdAsync(string username)
        {
            var user = await _dbContext.Usuarios.SingleOrDefaultAsync(u => u.Username == username);
            return user?.Id;
        }


    }
}
