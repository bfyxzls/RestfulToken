using System;
namespace Lind.Authorization
{
	/// <summary>
    /// 授权用户接口，具体业务用户表需要实现它
    /// </summary>
	public interface IUserDetails
    {
		string Username { get; set; }
		string Password { get; set; }
    }
}
