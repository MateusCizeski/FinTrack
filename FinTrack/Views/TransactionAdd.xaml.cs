using FinTrack.ViewModels;

namespace FinTrack.Views;

public partial class TransactionAdd : ContentPage
{
    public TransactionAdd(TransactionAddViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}