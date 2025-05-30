using Credutpay_Test.Dtos;
using Credutpay_Test.Entities;
using Credutpay_Test.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Credutpay_Test.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TransferController : ControllerBase
	{
		private readonly ITransferService _transferService;
		public TransferController(ITransferService transferService) => _transferService = transferService;

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateTransfer([FromBody] TransferRequest request)
		{
			var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!Guid.TryParse(userIdClaim, out var fromUserId))
				return Unauthorized("Token inválido.");

			try
			{
				await _transferService.TransferAsync(fromUserId, request.ToUserId, request.Amount);

				return Ok("Transação realizada.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> ListTransfers([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
		{
			string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (!Guid.TryParse(userIdClaim, out Guid userId))
				return Unauthorized("Token inválido.");

			try
			{
				IEnumerable<Transfer> transfers = await _transferService.ListTransfersAsync(userId, from, to);

				return Ok(transfers);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}