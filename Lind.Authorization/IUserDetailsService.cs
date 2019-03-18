using System;
namespace Lind.Authorization
{
	/// <summary>
    /// 授权对象获取接口
    /// </summary>
	public interface IUserDetailsService
	{
		IUserDetails LoadUserByUsername(String username);
	}
}
