using FinTrack.Models;
using FinTrack.Repositories;
using FinTrack.Libraries.Utils;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FinTrack.ViewModels
{
    public class CategoryItem
    {
        public TransactionCategory Category { get; init; }
        public string Emoji => Category.ToEmoji();
        public string Label => Category.ToLabel();
    }

    public abstract partial class TransactionFormViewModel : ObservableObject
    {
        protected readonly ITransactionRepository Repository;

        [ObservableProperty] private string _name = string.Empty;
        [ObservableProperty] private string _value = string.Empty;
        [ObservableProperty] private DateTime _date = DateTime.Today;
        [ObservableProperty] private bool _isIncome = true;
        [ObservableProperty] private bool _isExpense;
        [ObservableProperty] private string _errorMessage = string.Empty;
        [ObservableProperty] private bool _hasError;
        [ObservableProperty] private bool _isSaving;
        [ObservableProperty] private CategoryItem? _selectedCategory;

        public ObservableCollection<CategoryItem> Categories { get; } = new(
            Enum.GetValues<TransactionCategory>()
                .Select(c => new CategoryItem { Category = c }));

        protected TransactionFormViewModel(ITransactionRepository repository)
        {
            Repository = repository;
            SelectedCategory = Categories.FirstOrDefault();
        }

        [RelayCommand]
        public void SelectIncome() { IsIncome = true; IsExpense = false; }

        [RelayCommand]
        public void SelectExpense() { IsIncome = false; IsExpense = true; }

        [RelayCommand]
        public async Task SaveAsync()
        {
            var (isValid, error) = TransactionValidator.Validate(Name, Value);
            if (!isValid) { ErrorMessage = error; HasError = true; return; }

            IsSaving = true;
            HasError = false;
            try
            {
                await PersistAsync();
                KeyboardFixBugs.HideKeyboard();
                WeakReferenceMessenger.Default.Send<string>(string.Empty);
                await Application.Current!.MainPage!.Navigation.PopAsync();
            }
            finally { IsSaving = false; }
        }

        [RelayCommand]
        public async Task CloseAsync()
        {
            KeyboardFixBugs.HideKeyboard();
            await Application.Current!.MainPage!.Navigation.PopAsync();
        }

        protected TransactionType ResolvedType
            => IsIncome ? TransactionType.Income : TransactionType.Expense;

        protected TransactionCategory ResolvedCategory
            => SelectedCategory?.Category ?? TransactionCategory.Outros;

        protected abstract Task PersistAsync();
    }
}