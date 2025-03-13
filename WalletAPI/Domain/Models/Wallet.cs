namespace WalletAPI.Domain.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Transaction> SentTransactions { get; set; } = new List<Transaction>();
        public ICollection<Transaction> ReceivedTransactions { get; set; } = new List<Transaction>();

    }
}
