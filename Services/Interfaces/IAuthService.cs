namespace Credutpay_Test.Services.Interfaces
{
	public interface IAuthService
	{
		Task<string> LoginAsync(string username, string password);
	}
}
