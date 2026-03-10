using FinTrack.Models;
using FinTrack.ViewModels;

namespace FinTrack.Views;

public partial class TransactionList : ContentPage
{
    private readonly TransactionListViewModel _vm;

    public TransactionList(TransactionListViewModel viewModel)
    {
        InitializeComponent();
        _vm = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.ReloadAsync();
    }

    private void OnItemTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Transaction t)
            _ = _vm.EditTransactionCommand.ExecuteAsync(t);
    }

    private void OnDeleteTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Transaction t)
            _ = _vm.DeleteTransactionCommand.ExecuteAsync(t);
    }
}