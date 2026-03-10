namespace FinTrack
{
    public partial class App : Application
    {
        public App()
        {
            Resources["AppBg"] = Color.FromArgb("#0B0D11");
            Resources["Surface"] = Color.FromArgb("#13161D");
            Resources["Surface2"] = Color.FromArgb("#1A1E28");
            Resources["BorderColor"] = Color.FromArgb("#16FFFFFF");
            Resources["Green"] = Color.FromArgb("#00E5A0");
            Resources["GreenDim"] = Color.FromArgb("#1E00E5A0");
            Resources["Coral"] = Color.FromArgb("#FF5C6A");
            Resources["CoralDim"] = Color.FromArgb("#1EFF5C6A");
            Resources["Gold"] = Color.FromArgb("#F0C040");
            Resources["TextPrimary"] = Color.FromArgb("#E8EAF0");
            Resources["TextMuted"] = Color.FromArgb("#5A6070");

            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}