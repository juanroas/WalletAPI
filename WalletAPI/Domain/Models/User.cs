using Microsoft.AspNetCore.Identity;

namespace WalletAPI.Domain.Models
{
    public class User : IdentityUser
    {
        public Wallet Wallet { get; set; }
    }
}   