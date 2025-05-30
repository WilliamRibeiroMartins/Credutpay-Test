using Credutpay_Test.Infrastructure;
using Credutpay_Test.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Credutpay_Test.Services
{
	public class AuthService : IAuthService
	{
		private readonly AppDbContext _context;
		private readonly JwtHelper _jwt;
		public AuthService(AppDbContext context, JwtHelper jwt) { _context = context; _jwt = jwt; }

		public async Task<string> LoginAsync(string username, string password)
		{
			var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

			if (user == null || user.PasswordHash != JwtHelper.HashPassword(password))
				throw new UnauthorizedAccessException();

			string token = _jwt.GenerateToken(user);

			return token;
		}
	}
}
