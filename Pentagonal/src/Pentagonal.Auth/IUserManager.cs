using Pentagonal.Auth.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pentagonal.Auth
{
    /// <summary>
    /// A user manager service. Provides access to operations to manage user accounts.
    /// </summary>
    public interface IUserManager
    {
        // Query

        Task<RegisteredUser> FindUserByUsername(string username);

        Task<RegisteredUser> FindUserByApiKey(string apiKey);

        Task<bool> CheckPassword(string password, RegisteredUser user);

        Task<IEnumerable<RegisteredUser>> GetAllUsers();

        // Management

        Task<RegisteredUser> RegisterUser(RegistrationRequest request);

        Task<bool> UpdateUserInDatabase(RegisteredUser user);

        Task RemoveUser(string username);

        Task SetEnabled(RegisteredUser user, bool status);

        Task ChangePassword(RegisteredUser user, string newPassword);

        Task GenerateNewApiKey(RegisteredUser user);
    }
}