using Credutpay_Test.Entities;

namespace Credutpay_Test.Services.Interfaces;

public interface IUserService {

	Task<User> CreateUserAsync(string username, string password);

	Task<decimal> GetBalanceAsync(Guid userId);
	
	Task AddBalanceAsync(Guid userId, decimal amount);
}
