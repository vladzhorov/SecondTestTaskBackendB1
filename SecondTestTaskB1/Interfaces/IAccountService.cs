using SecondTestTaskB1.Models;

namespace SecondTestTaskB1.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<IEnumerable<Account>> GetAccountsByFileIdAsync(int fileId);
        Task AddAccountsAsync(IEnumerable<Account> accounts);
    }
}
