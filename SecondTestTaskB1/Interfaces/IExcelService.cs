using SecondTestTaskB1.Models;

namespace SecondTestTaskB1.Interfaces
{
    public interface IExcelService
    {
        Task<List<Account>> ProcessExcelFile(IFormFile file, int fileId);
    }
}