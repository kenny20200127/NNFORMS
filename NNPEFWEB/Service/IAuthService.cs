
using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NNPEFWEB.Services
{
    public interface IAuthService
    {
        Task<ResponseModel<User>> SignInUserAsync(string email, string password, string client);
        Task<User> FindUser(string Username);
        Task<User> FindUserByEmail(string Email);
        Task<bool> IsUserByEmailConfirmed(User user);
        Task<string> GeneratePasswordTokenAsync(User user);
        void updateUserlogins(User values);
        Task<List<User>> GetUsersAsync();
        Task SignOutUserAsync();
        Task<User> FindUserById(string id);
    }
}
