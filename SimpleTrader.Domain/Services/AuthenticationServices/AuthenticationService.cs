using Microsoft.AspNet.Identity;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> Login(string username, string password)
        {
            Account account = await _accountService.GetByUsername(username) ?? throw new UserNotFoundException("Invalid username");

            PasswordVerificationResult passwordVerificationResult = 
                _passwordHasher.VerifyHashedPassword(account.AccountHolder.PasswordHash, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new InvalidPasswordException("Invalid Password");

            return account;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                return RegistrationResult.PasswordDoNotMatch;
            }

            Account? existingAccount = await _accountService.GetByEmail(email);
            if (existingAccount != null)
            {
                return RegistrationResult.EmailAlreadyExists;
            }

            existingAccount = await _accountService.GetByUsername(username);
            if (existingAccount != null)
            {
                return RegistrationResult.UsernameAlreadyExists;
            }

            string hashedPassword = _passwordHasher.HashPassword(password);

            User user = new User
            {
                Email = email,
                Username = username,
                PasswordHash = hashedPassword,
                JoinedOn = DateTime.Now
            };  

            Account newAccount = new Account
            {
                AccountHolder = user,
                Balance = 100,
            };

            await _accountService.Create(newAccount);

            return RegistrationResult.Success;
        }
    }
}
