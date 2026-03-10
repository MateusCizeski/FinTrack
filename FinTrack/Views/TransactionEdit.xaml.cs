using FinTrack.ViewModels;

namespace FinTrack.Views;

public partial class TransactionEdit : ContentPage
{
    public TransactionEdit(TransactionEditViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}