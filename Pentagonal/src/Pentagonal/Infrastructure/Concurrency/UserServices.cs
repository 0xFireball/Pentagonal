using System.Collections.Generic;

namespace Pentagonal.Infrastructure.Concurrency
{
    public class UserServices
    {
        public string Username { get; }

        /// <summary>
        /// Read/write concurrency lock system
        /// </summary>
        public UserLock UserLock { get; }

        /// <summary>
        /// Resource throttle collection
        /// </summary>
        public List<ResourceThrottle> ResourceThrottles { get; }

        public UserServices(string username)
        {
            Username = username;
            UserLock = new UserLock();
            ResourceThrottles = new List<ResourceThrottle>();
        }
    }
}