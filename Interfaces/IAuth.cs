using Biblioteca_Api.Models;

namespace Biblioteca_Api.Interfaces
{
    public interface IAuth
    {
        Task<int> loginAsync(UserLogin user);
        Task<(string, UserModel)> RegistreAsync(UserModel user);
        Task<bool> UpdateAsync(UserModel user);
        Task<bool> DeleteUserAsync(int Id);
    }
}
