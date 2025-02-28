using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services
{
    public interface IAccountService : IDataService<Account>
    {
        Task<Account> GetByEmail(string email);

        Task<Account> GetByUsername(string username);
    }
}
