using Credutpay_Test.Entities;

namespace Credutpay_Test.Services.Interfaces
{
	public interface ITransferService
	{
		Task TransferAsync(Guid fromUserId, Guid toUserId, decimal amount);
		Task<IEnumerable<Transfer>> ListTransfersAsync(Guid userId, DateTime? from = null, DateTime? to = null);
	}
}
