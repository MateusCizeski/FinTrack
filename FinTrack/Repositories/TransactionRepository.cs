using Dapper;
using FinTrack.Database;
using FinTrack.Models;

namespace FinTrack.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DatabaseContext _context;

        public TransactionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            using var conn = _context.CreateConnection();
            var rows = await conn.QueryAsync<TransactionRow>(
                "SELECT * FROM transactions ORDER BY date DESC");
            return rows.Select(MapToDomain);
        }

        public async Task<IEnumerable<Transaction>> GetByMonthAsync(int year, int month)
        {
            using var conn = _context.CreateConnection();
            var rows = await conn.QueryAsync<TransactionRow>("""
                SELECT * FROM transactions
                WHERE strftime('%Y', date) = @Year
                  AND strftime('%m', date) = @Month
                ORDER BY date DESC
                """,
                new { Year = year.ToString("D4"), Month = month.ToString("D2") });
            return rows.Select(MapToDomain);
        }

        public async Task AddAsync(Transaction t)
        {
            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync("""
                INSERT INTO transactions (name, value, date, type, category)
                VALUES (@Name, @Value, @Date, @Type, @Category)
                """, MapToRow(t));
        }

        public async Task UpdateAsync(Transaction t)
        {
            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync("""
                UPDATE transactions
                SET name=@Name, value=@Value, date=@Date,
                    type=@Type, category=@Category
                WHERE id=@Id
                """, MapToRow(t));
        }

        public async Task DeleteAsync(int id)
        {
            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync("DELETE FROM transactions WHERE id=@Id", new { Id = id });
        }

        private sealed class TransactionRow
        {
            public long Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public double Value { get; set; }
            public string Date { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
        }

        private static Transaction MapToDomain(TransactionRow r) => new()
        {
            Id = (int)r.Id,
            Name = r.Name,
            Value = (decimal)r.Value,
            Date = DateTimeOffset.Parse(r.Date),
            Type = Enum.Parse<TransactionType>(r.Type),
            Category = Enum.TryParse<TransactionCategory>(r.Category, out var cat)
                           ? cat : TransactionCategory.Outros,
        };

        private static TransactionRow MapToRow(Transaction t) => new TransactionRow
        {
            Id = t.Id,
            Name = t.Name,
            Value = (double)t.Value,
            Date = t.Date.ToString("o"),
            Type = t.Type.ToString(),
            Category = t.Category.ToString()
        };
    }
}