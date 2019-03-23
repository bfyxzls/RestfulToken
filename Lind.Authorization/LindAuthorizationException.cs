using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lind.Authorization
{
    /// <summary>
    /// 从授权服务抛出来的异常
    /// </summary>
    public class LindAuthorizationException : Exception
    {
        public LindAuthorizationException(string message) 
            : base(message)
        {

        }
    }
}
