namespace Credutpay_Test.Entities;

public class User {

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public decimal Balance { get; set; } = 0.0m;

	public ICollection<Transfer> SentTransfers { get; set; }

	public ICollection<Transfer> ReceivedTransfers { get; set; }
}
