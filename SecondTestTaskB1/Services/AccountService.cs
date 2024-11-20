using Microsoft.EntityFrameworkCore;
using SecondTestTaskB1.Db;
using SecondTestTaskB1.Interfaces;
using SecondTestTaskB1.Models;

namespace SecondTestTaskB1.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
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
                .Where(a => a.FileId == fileId)
                .ToListAsync();
        }
        public async Task AddAccountsAsync(IEnumerable<Account> accounts)
        {
            await _context.Accounts.AddRangeAsync(accounts);
        }
    }
}