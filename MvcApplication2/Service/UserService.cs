using Lind.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication2.Models;
using Lind.DDD.Caching;

namespace MvcApplication2.Services
{
    [Lind.DI.Component]
    public class UserService : IUserDetailsService
    {
        public string GenerateToken(IUserDetails userDetails)
        {
            return "abc123";
        }

        public IUserDetails LoadUserByUsername(string username)
        {
            return new UserInfo
            {
                Id = 1,
                Username = "zzl",
                IsLock = false,
                Password = "abc123"
            };
        }

        public void ValidateToken(string token)
        {
            if (token != "abc123")
            {
                throw new LindAuthorizationException("token is not exist!");
            }
        }
    }
}