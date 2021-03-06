﻿using System;
namespace Lind.Authorization
{
    /// <summary>
    /// 授权处理程序
    /// </summary>
    [Lind.DI.Component]
    public  class TokenUserDetailsAuthenticationProvider:IUserDetailsAuthenticationProvider
	{
        IUserDetailsService userDetailsService;
        public TokenUserDetailsAuthenticationProvider(IUserDetailsService userDetailsService)
        {
            if(userDetailsService==null)
            {
                throw new ArgumentException("你需要注入IUserDetailsService的实例");
            }
            this.userDetailsService = userDetailsService;
        }

        /// <summary>
        /// 返回授权对象
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name="username">Username.</param>
		public  virtual IUserDetails RetrieveUser(string username)
        {
            var loadedUser = userDetailsService.LoadUserByUsername(username);
            if (loadedUser == null)
            {
                throw new LindAuthorizationException(
                    "UserDetailsService returned null, which is an interface contract violation");
            }
            if (loadedUser.IsLock)
            {
                throw new LindAuthorizationException(
                    "UserDetailsService is locked");
            }
            return loadedUser;
        }

        /// <summary>
        /// 校验对象
        /// </summary>
        /// <param name="userDetails">User details.</param>
        public  virtual void CheckUserDetails(IUserDetails userDetails)
        {
            if (userDetails.Password == null)
            {
                throw new ArgumentException("userDetails.password is null");
            }
        }

        /// <summary>
        /// 返回授权方式，在header头中的key
        /// </summary>
        /// <returns>The header prefix.</returns>
		public  virtual string GetHeaderPrefix()
		{
			return "Authorization";
		}

        public virtual bool ValidateAuthentication(string token)
        {
            userDetailsService.ValidateToken(token);
            return true;
        }

        public virtual string GetHeaderBodyPrefix()
        {
           return "Bearer ";
        }
    }
}
