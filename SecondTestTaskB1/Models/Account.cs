namespace SecondTestTaskB1.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public decimal OpeningActive { get; set; }
        public decimal OpeningPassive { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal ClosingActive { get; set; }
        public decimal ClosingPassive { get; set; }
        public string? AccountClass { get; set; }
        public int FileId { get; set; }
        public File? File { get; set; }
    }
}
