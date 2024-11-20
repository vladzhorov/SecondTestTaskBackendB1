using File = SecondTestTaskB1.Models.File;

namespace SecondTestTaskB1.Interfaces
{
    public interface IFileRepository
    {
        Task<IEnumerable<File>> GetAllFilesAsync();
        Task<File?> GetFileByIdAsync(int id);
        Task AddFileAsync(File file);
        Task<bool> SaveChangesAsync();
    }
}
