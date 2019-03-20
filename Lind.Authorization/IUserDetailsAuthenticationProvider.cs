namespace Lind.Authorization
{
    public interface IUserDetailsAuthenticationProvider
    {
        void CheckUserDetails(IUserDetails userDetails);
        IUserDetails RetrieveUser(string username);
		string getHeaderPrefix();
		bool validateAuthentication(string token);
    }
}