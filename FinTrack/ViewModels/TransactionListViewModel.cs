using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinTrack.Models;
using FinTrack.Repositories;
using FinTrack.Views;
using System.Collections.ObjectModel;

namespace FinTrack.ViewModels
{
    public partial class TransactionListViewModel : ObservableObject
    {
        private readonly ITransactionRepository _repository;

        [ObservableProperty] private ObservableCollection<Transaction> _transactions = new();
        [ObservableProperty] private decimal _income;
        [ObservableProperty] private decimal _expense;
        [ObservableProperty] private decimal _balance;
        [ObservableProperty] private bool _isPositiveBalance = true;
        [ObservableProperty] private bool _isLoading;
        [ObservableProperty] private int _selectedYear = DateTime.Today.Year;
        [ObservableProperty] private int _selectedMonth = DateTime.Today.Month;
        [ObservableProperty] private string _monthLabel = string.Empty;

        public TransactionListViewModel(ITransactionRepository repository)
        {
            _repository = repository;
            UpdateMonthLabel();
            _ = ReloadAsync();
            WeakReferenceMessenger.Default.Register<string>(this,
                async (_, _) => await ReloadAsync());
        }

        [RelayCommand]
        public async Task PreviousMonthAsync()
        {
            var d = new DateTime(SelectedYear, SelectedMonth, 1).AddMonths(-1);
            SelectedYear = d.Year; SelectedMonth = d.Month;
            UpdateMonthLabel();
            await ReloadAsync();
        }

        [RelayCommand]
        public async Task NextMonthAsync()
        {
            var d = new DateTime(SelectedYear, SelectedMonth, 1).AddMonths(1);
            SelectedYear = d.Year; SelectedMonth = d.Month;
            UpdateMonthLabel();
            await ReloadAsync();
        }

        [RelayCommand]
        public async Task AddTransactionAsync()
        {
            var page = IPlatformApplication.Current!.Services.GetRequiredService<TransactionAdd>();
            await Application.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        public async Task EditTransactionAsync(Transaction t)
        {
            var vm = IPlatformApplication.Current!.Services.GetRequiredService<TransactionEditViewModel>();
            await vm.LoadAsync(t.Id);
            var page = IPlatformApplication.Current!.Services.GetRequiredService<TransactionEdit>();
            await Application.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        public async Task DeleteTransactionAsync(Transaction t)
        {
            bool ok = await Shell.Current.DisplayAlert("Excluir", "Tem certeza?", "Sim", "Não");

            if (!ok) return;
            
            await _repository.DeleteAsync(t.Id);
            await ReloadAsync();
        }

        public async Task ReloadAsync()
        {
            IsLoading = true;
            try
            {
                var items = (await _repository
                    .GetByMonthAsync(SelectedYear, SelectedMonth)).ToList();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Transactions = new ObservableCollection<Transaction>(items);
                    Income = items.Where(t => t.Type == TransactionType.Income).Sum(t => t.Value);
                    Expense = items.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Value);
                    Balance = Income - Expense;
                    IsPositiveBalance = Balance >= 0;
                });
            }
            finally { IsLoading = false; }
        }

        private void UpdateMonthLabel() =>
            MonthLabel = new DateTime(SelectedYear, SelectedMonth, 1)
                .ToString("MMMM yyyy", new System.Globalization.CultureInfo("pt-BR"));
    }
}