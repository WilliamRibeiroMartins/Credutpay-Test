using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Credutpay_Test.Services.Interfaces;
using Credutpay_Test.Dtos;

namespace Credutpay_Test.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService) => _userService = userService;

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			var user = await _userService.CreateUserAsync(request.Username, request.Password);
			return Ok(new { user.Id, user.Username });
		}

		[Authorize]
		[HttpGet("balance")]
		public async Task<IActionResult> GetBalance()
		{
			string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (!Guid.TryParse(userIdClaim, out Guid userId))
				return Unauthorized("Token inválido.");

			try
			{
				var balance = await _userService.GetBalanceAsync(userId);

				return Ok(new { balance });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			
		}

		[Authorize]
		[HttpPost("add-balance")]
		public async Task<IActionResult> AddBalance([FromBody] AddBalanceRequest request)
		{
			string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (!Guid.TryParse(userIdClaim, out Guid userId))
				return Unauthorized("Token inválido.");

			try
			{
				await _userService.AddBalanceAsync(userId, request.Amount);
				return Ok("Depósito realizado.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			
		}
	}
}
