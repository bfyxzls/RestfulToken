using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2.Models
{
    public class UserInfo : Lind.Authorization.IUserDetails
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLock { get; set; }
    }
}