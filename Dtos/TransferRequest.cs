namespace Credutpay_Test.Dtos
{
	public record TransferRequest(Guid ToUserId, decimal Amount);
}
