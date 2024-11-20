using SecondTestTaskB1.Models;
using File = SecondTestTaskB1.Models.File;


namespace SecondTestTaskB1.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<File>> GetAllFilesAsync();
        Task<File> GetFileByIdAsync(int id);
        Task AddFileAsync(string fileName, DateTime uploadDate, List<Account> accounts);
        Task<IEnumerable<Account>> GetFileAccountsAsync(int fileId);
        Task UploadFile(IFormFile file);
    }
}
