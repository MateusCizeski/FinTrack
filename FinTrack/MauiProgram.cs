using FinTrack.Database;
using FinTrack.Repositories;
using FinTrack.ViewModels;
using FinTrack.Views;
using Microsoft.Extensions.Logging;

namespace FinTrack
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterDatabase()
                .RegisterViewModels()
                .RegisterViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterDatabase(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<DatabaseContext>();
            builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
            return builder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<TransactionListViewModel>();
            builder.Services.AddTransient<TransactionAddViewModel>();
            builder.Services.AddTransient<TransactionEditViewModel>();
            return builder;
        }

        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<TransactionList>();
            builder.Services.AddTransient<TransactionAdd>();
            builder.Services.AddTransient<TransactionEdit>();

            Routing.RegisterRoute("TransactionAdd", typeof(TransactionAdd));
            Routing.RegisterRoute("TransactionEdit", typeof(TransactionEdit));
            return builder;
        }
    }
}