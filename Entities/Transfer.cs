namespace Credutpay_Test.Entities
{
	public class Transfer
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public Guid FromUserId { get; set; }
		public Guid ToUserId { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; } = DateTime.UtcNow;

		public User FromUser { get; set; }
		public User ToUser { get; set; }
	}
}
