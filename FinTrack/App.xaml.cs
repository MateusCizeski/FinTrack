using FinTrack.Views;

namespace FinTrack
{
    public partial class App : Application
    {
        public App(TransactionList transactionList)
        {
            InitializeComponent();
            MainPage = new NavigationPage(transactionList);
        }
    }
}