using System;
namespace Lind.Authorization
{
	/// <summary>
	/// 默认的权限提供者
	/// </summary>
	public class DefaultUserDetailsAuthenticationProvider
		: BaseUserDetailsAuthenticationProvider, IUserDetailsAuthenticationProvider
	{
		public DefaultUserDetailsAuthenticationProvider(IUserDetailsService userDetailsService)
			: base(userDetailsService)
		{
		}
        
		/// <summary>
        /// token类型前缀
        /// </summary>
        public const string TOKEN_PREFIX = "Bearer ";
       

        /// <summary>
        /// 校验token
        /// </summary>
        /// <param name="token">Token.</param>
		public bool validateAuthentication(string token)
		{
			//校验token有效性
			return true;
		}
	}
}
