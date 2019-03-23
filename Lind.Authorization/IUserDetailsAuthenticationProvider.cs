namespace Lind.Authorization
{
    /// <summary>
    /// 授权提供者
    /// </summary>
    public interface IUserDetailsAuthenticationProvider
    {
        void CheckUserDetails(IUserDetails userDetails);
        IUserDetails RetrieveUser(string username);
		string getHeaderPrefix();
		bool validateAuthentication(string token);
    }
}