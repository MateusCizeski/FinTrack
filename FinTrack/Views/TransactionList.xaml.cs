using FinTrack.ViewModels;

namespace FinTrack.Views;

public partial class TransactionList : ContentPage
{
    private TransactionListViewModel _vm;

    public TransactionList()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_vm == null)
        {
            _vm = IPlatformApplication.Current.Services
                      .GetRequiredService<TransactionListViewModel>();
            BindingContext = _vm;
        }

        await _vm.ReloadAsync();
    }

    private void OnItemTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is FinTrack.Models.Transaction t)
            _ = _vm.EditTransactionCommand.ExecuteAsync(t);
    }

    private void OnDeleteTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is FinTrack.Models.Transaction t)
            _ = _vm.DeleteTransactionCommand.ExecuteAsync(t);
    }
}