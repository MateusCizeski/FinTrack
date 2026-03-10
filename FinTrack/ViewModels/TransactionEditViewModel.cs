using FinTrack.Models;
using FinTrack.Repositories;

namespace FinTrack.ViewModels
{
    [QueryProperty(nameof(TransactionId), "transactionId")]
    public partial class TransactionEditViewModel : TransactionFormViewModel
    {
        private Transaction? _original;

        public int TransactionId { set => _ = LoadAsync(value); }

        public TransactionEditViewModel(ITransactionRepository repository) : base(repository) { }

        private async Task LoadAsync(int id)
        {
            var all = await Repository.GetAllAsync();
            _original = all.FirstOrDefault(t => t.Id == id);
            if (_original is null) return;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Name = _original.Name;
                Value = _original.Value.ToString();
                Date = _original.Date.DateTime;
                IsIncome = _original.Type == TransactionType.Income;
                IsExpense = !IsIncome;
                SelectedCategory = Categories.FirstOrDefault(c => c.Category == _original.Category) ?? Categories.FirstOrDefault();
            });
        }

        protected override Task PersistAsync() =>
            Repository.UpdateAsync(new Transaction
            {
                Id = _original!.Id,
                Name = Name,
                Type = ResolvedType,
                Category = ResolvedCategory,
                Date = new DateTimeOffset(Date),
                Value = decimal.Parse(Value),
            });
    }
}