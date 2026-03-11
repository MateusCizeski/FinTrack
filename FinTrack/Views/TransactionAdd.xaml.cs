using FinTrack.ViewModels;

namespace FinTrack.Views;

public partial class TransactionAdd : ContentPage
{
    public TransactionAdd()
    {
        InitializeComponent();
        BindingContext = IPlatformApplication.Current.Services
                            .GetRequiredService<TransactionAddViewModel>();
    }
}