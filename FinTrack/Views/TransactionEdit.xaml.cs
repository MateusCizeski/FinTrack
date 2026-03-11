using FinTrack.ViewModels;

namespace FinTrack.Views;

public partial class TransactionEdit : ContentPage
{
    public TransactionEdit()
    {
        InitializeComponent();
        BindingContext = IPlatformApplication.Current.Services
                            .GetRequiredService<TransactionEditViewModel>();
    }
}