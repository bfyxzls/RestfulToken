namespace Lind.Authorization
{
	/// <summary>
	/// 授权提供者
	/// </summary>
	public interface IUserDetailsAuthenticationProvider
	{
		/// <summary>
		/// 验证用户对象有效性，用户名，密码策略校验.
		/// </summary>
		/// <param name="userDetails">User details.</param>
		void CheckUserDetails(IUserDetails userDetails);
		/// <summary>
		/// 通过唯一的用户名，获取用户实体.
		/// </summary>
		/// <returns>The user.</returns>
		/// <param name="username">Username.</param>
		IUserDetails RetrieveUser(string username);
		/// <summary>
		/// 获取http头的标识.
		/// </summary>
		/// <returns>The header prefix.</returns>
		string GetHeaderPrefix();
		/// <summary>
		/// 校验逻辑.
		/// </summary>
		/// <returns><c>true</c>, if authentication was validated, <c>false</c> otherwise.</returns>
		/// <param name="token">Token.</param>
		bool ValidateAuthentication(string token);
		/// <summary>
		/// 获取密钥的前缀.
		/// </summary>
		/// <returns>The header body prefix.</returns>
		string GetHeaderBodyPrefix();
	}
}