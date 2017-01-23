using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pentagonal.Auth.Models;

namespace Pentagonal.Auth.Services
{
    public class WebUserManager : IUserManager
    {
        public Task ChangePassword(RegisteredUser user, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPassword(string password, RegisteredUser user)
        {
            throw new NotImplementedException();
        }

        public Task<RegisteredUser> FindUserByApiKey(string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<RegisteredUser> FindUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task GenerateNewApiKeyAsync(RegisteredUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RegisteredUser>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RegisteredUser> RegisterUser(RegistrationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task SetEnabled(RegisteredUser user, bool status)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserInDatabase(RegisteredUser user)
        {
            throw new NotImplementedException();
        }
    }
}