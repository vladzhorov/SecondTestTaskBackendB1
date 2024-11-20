using Microsoft.AspNetCore.Mvc;
using SecondTestTaskB1.Interfaces;
using File = SecondTestTaskB1.Models.File;


namespace SecondTestTaskB1.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("files")]
        public async Task<IEnumerable<File>> GetAllFiles()
        {
            return await _fileService.GetAllFilesAsync();
        }

        [HttpGet("files/{id}")]
        public async Task<File> GetFileById(int id)
        {
            return await _fileService.GetFileByIdAsync(id);
        }
        //Знаю что exceptions не красивые но для одного делать middleware как то ту мач
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            await _fileService.UploadFile(file);
            return Ok(new { message = "File uploaded successfully." });
        }
    }
}