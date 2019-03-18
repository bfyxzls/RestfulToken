using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lind.Authorization
{
    public class LindAuthorizationException : Exception
    {
        public LindAuthorizationException(string message) 
            : base(message)
        {

        }
    }
}
