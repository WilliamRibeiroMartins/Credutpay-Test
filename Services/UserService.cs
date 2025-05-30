using Credutpay_Test.Entities;
using Credutpay_Test.Infrastructure;
using Credutpay_Test.Services.Interfaces;

namespace Credutpay_Test.Services;

public class UserService : IUserService {

	private readonly AppDbContext _context;
	public UserService(AppDbContext context) => _context = context;

	public async Task<User> CreateUserAsync(string username, string password)
	{
		var user = new User { Username = username, PasswordHash = JwtHelper.HashPassword(password) };
		
		_context.Users.Add(user);
		await _context.SaveChangesAsync();
		
		return user;
	}

	public async Task<decimal> GetBalanceAsync(Guid userId)
	{
		var user = await _context.Users.FindAsync(userId);

		return user?.Balance ?? 0;
	}

	public async Task AddBalanceAsync(Guid userId, decimal amount)
	{
		var user = await _context.Users.FindAsync(userId);
		
		if (user != null)
		{
			user.Balance += amount;
			await _context.SaveChangesAsync();
		}
	}
}
