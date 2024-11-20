using Microsoft.EntityFrameworkCore;
using SecondTestTaskB1.Db;
using SecondTestTaskB1.Interfaces;
using File = SecondTestTaskB1.Models.File;


namespace SecondTestTaskB1.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<File>> GetAllFilesAsync()
        {
            return await _context.Files.Include(f => f.Accounts).ToListAsync();
        }

        public async Task<File?> GetFileByIdAsync(int id)
        {
            return await _context.Files.Include(f => f.Accounts).FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<File?> GetFileByFileNameAsync(string fileName)
        {
            return await _context.Files
                .FirstOrDefaultAsync(f => f.FileName == fileName);
        }

        public async Task AddFileAsync(File file)
        {
            if (file != null)
            {
                //Конвертирование под UTF в бд
                if (file?.UploadDate != null)
                {
                    file.UploadDate = file.UploadDate.ToUniversalTime();
                }

            }

            await _context.Files.AddAsync(file!);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
