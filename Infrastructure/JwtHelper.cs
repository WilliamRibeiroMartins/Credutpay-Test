using Credutpay_Test.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Credutpay_Test.Infrastructure
{
	public class JwtHelper
	{
		private readonly string _key;

		public JwtHelper(IConfiguration configuration) => _key = configuration["Jwt:Key"];

		public string GenerateToken(User user)
		{
			var claims = new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddHours(2),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public static string HashPassword(string password)
		{
			using var sha = SHA256.Create();
			
			var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
			
			return Convert.ToBase64String(bytes);
		}
	}
}
