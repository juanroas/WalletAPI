using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WalletAPI.Application.DTOs;
using WalletAPI.Domain.Models;

namespace WalletAPI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<(bool Success, string Message)> RegisterUser(string email, string password)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
                return (false, "Usuário já existe.");

            var user = new User { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded ? (true, "Usuário registrado com sucesso.") : (false, string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return result.Succeeded;
        }
    }
}
