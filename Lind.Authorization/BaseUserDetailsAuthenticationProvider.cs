using System;
namespace Lind.Authorization
{
    /// <summary>
    /// 授权处理程序
    /// </summary>
    public abstract class BaseUserDetailsAuthenticationProvider
    {
        IUserDetailsService userDetailsService;
        public BaseUserDetailsAuthenticationProvider(IUserDetailsService userDetailsService)
        {
            this.userDetailsService = userDetailsService;
        }

        /// <summary>
        /// 返回授权对象
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name="username">Username.</param>
		public virtual IUserDetails RetrieveUser(string username)
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
        public virtual void CheckUserDetails(IUserDetails userDetails)
        {
            if (userDetails.Password == null)
            {
                throw new ArgumentException("userDetails.password is null");
            }
        }

    }
}
