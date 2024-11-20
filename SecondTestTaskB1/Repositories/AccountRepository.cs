using Microsoft.EntityFrameworkCore;
using SecondTestTaskB1.Db;
using SecondTestTaskB1.Interfaces;
using SecondTestTaskB1.Models;

namespace SecondTestTaskB1.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<IEnumerable<Account>> GetAccountsByFileIdAsync(int fileId)
        {
            return await _context.Accounts
                .Where(account => account.FileId == fileId)
                .ToListAsync();
        }

        public async Task AddAccountsAsync(IEnumerable<Account> accounts)
        {
            await _context.Accounts.AddRangeAsync(accounts);
            await _context.SaveChangesAsync();
        }
    }
}
