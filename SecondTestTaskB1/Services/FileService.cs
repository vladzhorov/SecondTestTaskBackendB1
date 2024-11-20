using SecondTestTaskB1.Interfaces;
using SecondTestTaskB1.Models;
using File = SecondTestTaskB1.Models.File;


namespace SecondTestTaskB1.Services
{
    /// <summary>
    /// Сервис для управления файлами и их связанными данными.
    /// </summary>
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IAccountService _accountService;
        private readonly IExcelService _excelService;

        public FileService(IFileRepository fileRepository, IAccountService accountService, IExcelService excelService)
        {
            _fileRepository = fileRepository;
            _accountService = accountService;
            _excelService = excelService;
        }

        public async Task<IEnumerable<File>> GetAllFilesAsync()
        {
            return await _fileRepository.GetAllFilesAsync();
        }

        public async Task<File> GetFileByIdAsync(int id)
        {
            var file = await _fileRepository.GetFileByIdAsync(id);

            if (file == null)
            {
                throw new KeyNotFoundException($"File with ID {id} not found.");
            }

            return file;
        }

        public async Task AddFileAsync(string fileName, DateTime uploadDate, List<Account> accounts)
        {
            var file = new File
            {
                FileName = fileName,
                UploadDate = uploadDate,
                Accounts = accounts
            };

            await _fileRepository.AddFileAsync(file);
            await _fileRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Account>> GetFileAccountsAsync(int fileId)
        {
            var file = await _fileRepository.GetFileByIdAsync(fileId);

            if (file == null)
            {
                throw new KeyNotFoundException($"File with ID {fileId} not found.");
            }

            return file.Accounts;
        }

        /// <summary>
        /// Метод для загрузки файла на сервер, обработки его содержимого и сохранения данных в базе данных.
        /// </summary>
        public async Task UploadFile(IFormFile file)
        {
            // Получаем имя файла и путь для сохранения на сервере
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", fileName);

            // Сохраняем файл на сервере в папку "UploadedFiles"
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream); // Копируем содержимое файла в поток
            }

            // Обрабатываем Excel файл и получаем список аккаунтов
            var accounts = await _excelService.ProcessExcelFile(file, 0);

            // Создаем запись о загруженном файле
            var fileRecord = new File
            {
                FileName = fileName,
                UploadDate = DateTime.Now,
                Accounts = accounts
            };

            await _fileRepository.AddFileAsync(fileRecord);
            await _fileRepository.SaveChangesAsync();

            // Присваиваем каждому аккаунту ID загруженного файла
            foreach (var account in accounts)
            {
                account.FileId = fileRecord.Id;
            }

            await _accountService.AddAccountsAsync(accounts);
        }


    }
}
