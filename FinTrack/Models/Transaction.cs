namespace FinTrack.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset Date { get; set; }
        public decimal Value { get; set; }
        public TransactionType Type { get; set; }
        public TransactionCategory Category { get; set; } = TransactionCategory.Outros;
    }

    public enum TransactionType { Income, Expense }
}