using AuthenticationAPI.Core.Contract.Logic;
using AuthenticationAPI.Core.Enums;
using System.Collections.Generic;

namespace AuthenticationAPI.Application.Logic
{
    public class UserService : IUserService
    {
        private readonly IDictionary<string, string> _users = new Dictionary<string, string>
        {
            { "test1", "test1" },
            { "test2", "test2" },
            { "admin", "admin" }
        };

        public bool IsValidUserCredentials(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            return _users.TryGetValue(userName, out var p) && p == password;
        }

        public bool IsAnExistingUser(string userName)
        {
            return _users.ContainsKey(userName);
        }

        public string GetUserRole(string userName)
        {
            if (!IsAnExistingUser(userName))
            {
                return string.Empty;
            }

            if (userName == "admin")
            {
                return UserRoles.Admin;
            }

            return UserRoles.BasicUser;
        }
    }
}
