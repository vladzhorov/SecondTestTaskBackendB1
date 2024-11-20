namespace SecondTestTaskB1.Models
{
    public class File
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
