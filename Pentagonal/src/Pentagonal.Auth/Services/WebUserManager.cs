using PenguinUpload.Utilities;
using Pentagonal.Auth.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;

namespace Pentagonal.Auth.Services
{
    public class WebUserManager : IUserManager
    {
        public async Task<RegisteredUser> FindUserByUsername(string username)
        {
            return await Task.Run(() =>
            {
                RegisteredUser storedUserRecord = null;
                var db = PentagonalAuthenticationServices.Configuration.Database;
                var registeredUsers =
                    db.GetCollection<RegisteredUser>(PentagonalAuthenticationServices.Configuration.UserTableName);
                var userRecord = registeredUsers.FindOne(u => u.Username == username);
                storedUserRecord = userRecord;

                return storedUserRecord;
            });
        }

        public async Task<RegisteredUser> FindUserByApiKey(string apiKey)
        {
            return await Task.Run(() =>
            {
                RegisteredUser storedUserRecord = null;
                var db = PentagonalAuthenticationServices.Configuration.Database;
                var registeredUsers = db.GetCollection<RegisteredUser>(PentagonalAuthenticationServices.Configuration.UserTableName);
                var userRecord = registeredUsers.FindOne(u => u.ApiKey == apiKey);
                storedUserRecord = userRecord;

                return storedUserRecord ?? null;
            });
        }

        /// <summary>
        /// Warning: Callers are expected to use their own locks before calling this method!
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserInDatabase(RegisteredUser user)
        {
            var result = false;
            await Task.Run(() =>
            {
                var db = PentagonalAuthenticationServices.Configuration.Database;
                var registeredUsers =
                    db.GetCollection<RegisteredUser>(PentagonalAuthenticationServices.Configuration.UserTableName);
                using (var trans = db.BeginTrans())
                {
                    result = registeredUsers.Update(user);
                    trans.Commit();
                }
            });
            return result;
        }

        /// <summary>
        /// Attempts to register a new user. Only the username is validated, it is expected that other fields have already been validated!
        /// </summary>
        public async Task<RegisteredUser> RegisterUser(RegistrationRequest request)
        {
            return await Task.Run(() =>
            {
                RegisteredUser newUserRecord = null;
                if (FindUserByUsername(request.Username).GetAwaiter().GetResult() != null)
                {
                    //BAD! Another conflicting user exists!
                    throw new SecurityException("A user with the same username already exists!");
                }
                var db = PentagonalAuthenticationServices.Configuration.Database;
                var registeredUsers = db.GetCollection<RegisteredUser>(PentagonalAuthenticationServices.Configuration.UserTableName);
                using (var trans = db.BeginTrans())
                {
                    // Calculate cryptographic info
                    var cryptoConf = PasswordCryptoConfiguration.CreateDefault();
                    var pwSalt = AuthCryptoHelper.GetRandomSalt(AuthCryptoHelper.DefaultSaltLength);
                    var encryptedPassword =
                        AuthCryptoHelper.CalculateUserPasswordHash(request.Password, pwSalt, cryptoConf);
                    // Create user
                    newUserRecord = new RegisteredUser
                    {
                        Identifier = Guid.NewGuid().ToString(),
                        Username = request.Username,
                        ApiKey = StringUtils.SecureRandomString(AuthCryptoHelper.DefaultApiKeyLength),
                        CryptoSalt = pwSalt,
                        PasswordCryptoConf = cryptoConf,
                        PasswordKey = encryptedPassword
                    };
                    // Add the user to the database
                    registeredUsers.Insert(newUserRecord);

                    // Index database
                    registeredUsers.EnsureIndex(x => x.Identifier);
                    registeredUsers.EnsureIndex(x => x.ApiKey);
                    registeredUsers.EnsureIndex(x => x.Username);

                    trans.Commit();
                }
                return newUserRecord;
            });
        }

        public async Task<bool> CheckPassword(string password, RegisteredUser user)
        {
            var ret = false;
            var lockEntry = PentagonalServices.Configuration.ServiceTable.GetOrCreate(user.Username).UserLock;
            await lockEntry.WithConcurrentRead(Task.Run(() =>
            {
                //Calculate hash and compare
                var pwKey =
                    AuthCryptoHelper.CalculateUserPasswordHash(password, user.CryptoSalt,
                        user.PasswordCryptoConf);
                ret = StructuralComparisons.StructuralEqualityComparer.Equals(pwKey, user.PasswordKey);
            }));
            return ret;
        }

        public async Task RemoveUser(string username)
        {
            await Task.Run(() =>
            {
                var db = PentagonalAuthenticationServices.Configuration.Database;
                var registeredUsers =
                    db.GetCollection<RegisteredUser>(PentagonalAuthenticationServices.Configuration.UserTableName);
                using (var trans = db.BeginTrans())
                {
                    registeredUsers.Delete(u => u.Username == username);
                    trans.Commit();
                }
            });
        }

        public async Task SetEnabled(RegisteredUser user, bool status)
        {
            var lockEntry = PentagonalServices.Configuration.ServiceTable.GetOrCreate(user.Username).UserLock;
            await lockEntry.ObtainExclusiveWriteAsync();
            user.Enabled = status;
            await UpdateUserInDatabase(user);
            lockEntry.ReleaseExclusiveWrite();
        }

        public async Task ChangePassword(RegisteredUser user, string newPassword)
        {
            var lockEntry = PentagonalServices.Configuration.ServiceTable.GetOrCreate(user.Username).UserLock;
            await lockEntry.WithExclusiveWrite(Task.Run(() =>
            {
                // Recompute password crypto
                var cryptoConf = PasswordCryptoConfiguration.CreateDefault();
                var salt = AuthCryptoHelper.GetRandomSalt(AuthCryptoHelper.DefaultSaltLength);
                var encryptedPassword =
                    AuthCryptoHelper.CalculateUserPasswordHash(newPassword, salt, cryptoConf);
                user.CryptoSalt = salt;
                user.PasswordCryptoConf = cryptoConf;
                user.PasswordKey = encryptedPassword;
            }));
        }

        public async Task GenerateNewApiKey(RegisteredUser user)
        {
            var lockEntry = PentagonalServices.Configuration.ServiceTable.GetOrCreate(user.Username).UserLock;
            await lockEntry.WithExclusiveWrite(Task.Run(() =>
            {
                // Recompute key
                user.ApiKey = StringUtils.SecureRandomString(AuthCryptoHelper.DefaultApiKeyLength);
            }));
        }

        public async Task<IEnumerable<RegisteredUser>> GetAllUsers()
        {
            return await Task.Run(() =>
            {
                var db = PentagonalAuthenticationServices.Configuration.Database;
                var registeredUsers =
                    db.GetCollection<RegisteredUser>(PentagonalAuthenticationServices.Configuration.UserTableName);
                return registeredUsers.FindAll();
            });
        }
    }
}