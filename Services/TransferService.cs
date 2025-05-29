using Credutpay_Test.Entities;
using Credutpay_Test.Infrastructure;
using Credutpay_Test.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Credutpay_Test.Services
{
	public class TransferService : ITransferService
	{
		private readonly AppDbContext _context;
		public TransferService(AppDbContext context) => _context = context;

		public async Task TransferAsync(Guid fromUserId, Guid toUserId, decimal amount)
		{
			var fromUser = await _context.Users.FindAsync(fromUserId);
			var toUser = await _context.Users.FindAsync(toUserId);
			
			if (fromUser == null || toUser == null || fromUser.Balance < amount)
				throw new Exception("Invalid transfer");

			fromUser.Balance -= amount;
			toUser.Balance += amount;

			_context.Transfers.Add(new Transfer { FromUserId = fromUserId, ToUserId = toUserId, Amount = amount });
			
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Transfer>> ListTransfersAsync(Guid userId, DateTime? from = null, DateTime? to = null)
		{
			var query = _context.Transfers
							.Where(t => t.FromUserId == userId || t.ToUserId == userId);
			
			if (from.HasValue)
				query = query.Where(t => t.Date >= from);
			
			if (to.HasValue)
				query = query.Where(t => t.Date <= to);
			
			return await query.ToListAsync();
		}
	}
}
