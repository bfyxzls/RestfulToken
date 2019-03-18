using System;
namespace Lind.Authorization
{
	/// <summary>
    /// 授权用户接口，具体业务用户表需要实现它
    /// </summary>
	public interface IUserDetails
    {
        /// <summary>
        /// 用户名
        /// </summary>
		string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
		string Password { get; set; }
        /// <summary>
        /// 用户是否锁定
        /// </summary>
        bool IsLock { get; set; }
    }
}
