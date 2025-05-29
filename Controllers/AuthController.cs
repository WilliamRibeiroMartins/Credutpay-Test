using Microsoft.AspNetCore.Mvc;
using Credutpay_Test.Services.Interfaces;
using Credutpay_Test.Dtos;
using Credutpay_Test.Services;

namespace Credutpay_Test.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService) => _authService = authService;

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			try
			{
				var token = await _authService.LoginAsync(request.Username, request.Password);
				return Ok(new { token });
			}
			catch
			{
				return Unauthorized();
			}
		}
	}
}
