using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services.Common;

namespace SimpleTrader.EntityFramework.Services
{
    public class AccountDataService : IAccountService
    {
        private readonly ApplicationDBContextFactory _contextFactory;
        private readonly NonQueryDataService<Account> _nonQueryDataService;

        public AccountDataService(ApplicationDBContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Account>(contextFactory);
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using ApplicationDBContext context = _contextFactory.CreateDbContext();
            IEnumerable<Account> entities = await context.Set<Account>()
                .Include(e => e.AccountHolder)
                .Include(e => e.AssetTransactions)
                .ToListAsync();
            return entities;
        }

        public async Task<Account> GetById(int id)
        {
            using ApplicationDBContext context = _contextFactory.CreateDbContext();
            Account? entity = await context.Set<Account>()
                .Include(e => e.AccountHolder)
                .Include(e => e.AssetTransactions)
                .FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<Account> GetByEmail(string email)
        {
            using ApplicationDBContext context = _contextFactory.CreateDbContext();
            Account? entity = await context.Set<Account>()
                .Include(e => e.AccountHolder)
                .Include(e => e.AssetTransactions)
                .FirstOrDefaultAsync(e => e.AccountHolder.Email == email);
            return entity;
        }

        public async Task<Account> GetByUsername(string username)
        {
            using ApplicationDBContext context = _contextFactory.CreateDbContext();
            Account? entity = await context.Set<Account>()
                .Include(e => e.AccountHolder)
                .Include(e => e.AssetTransactions)
                .FirstOrDefaultAsync(e => e.AccountHolder.Username == username);
            return entity;
        }

        public async Task<Account> Create(Account entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<Account> Update(int id, Account entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }
    }
}
