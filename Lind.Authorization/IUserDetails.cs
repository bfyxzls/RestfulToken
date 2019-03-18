using System;
namespace Lind.Authorization
{
	public interface IUserDetails
    {
		string Username { get; set; }
		string Password { get; set; }
    }
}
