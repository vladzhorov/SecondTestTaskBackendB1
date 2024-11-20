using Microsoft.AspNetCore.Mvc;
using SecondTestTaskB1.Interfaces;
using SecondTestTaskB1.Models;

namespace SecondTestTaskB1.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _accountService.GetAllAccountsAsync();
        }

        [HttpGet("by-file/{fileId}")]
        public async Task<IEnumerable<Account>> GetAccountsByFileId(int fileId)
        {
            return await _accountService.GetAccountsByFileIdAsync(fileId);
        }
    }
}
