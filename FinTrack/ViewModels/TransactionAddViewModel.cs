using FinTrack.Models;
using FinTrack.Repositories;

namespace FinTrack.ViewModels
{
    public partial class TransactionAddViewModel : TransactionFormViewModel
    {
        public TransactionAddViewModel(ITransactionRepository repository)
            : base(repository) { }

        protected override Task PersistAsync() =>
            Repository.AddAsync(new Transaction
            {
                Name = Name,
                Type = ResolvedType,
                Category = ResolvedCategory,
                Date = new DateTimeOffset(Date),
                Value = decimal.Parse(Value),
            });
    }
}