namespace WalletAPI.Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public int FromWalletId { get; set; }
        public Wallet FromWallet { get; set; }

        public int ToWalletId { get; set; }
        public Wallet ToWallet { get; set; }
    }
}
