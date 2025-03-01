using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinanceAPI.Services;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Assets;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
using System.Windows;

namespace SimpleTrader.WPF;

public partial class App : Application
{
    protected override async void OnStartup(StartupEventArgs e)
    {

        IServiceProvider serviceProvider = CreateServiceProvider();

        Window window = serviceProvider.GetRequiredService<MainWindow>();

        window.Show();

        base.OnStartup(e);
    }

    private IServiceProvider CreateServiceProvider()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddSingleton<ApplicationDBContextFactory>();

        services.AddSingleton<IDataService<Account>, AccountDataService>();
        services.AddSingleton<IAccountService, AccountDataService>();
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<IStockPriceService, StockPriceService>();
        services.AddSingleton<IBuyStockService, BuyStockService>();
        services.AddSingleton<IMajorIndexService, MajorIndexService>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddSingleton<IViewModelFactory, ViewModelFactory>();
        services.AddSingleton<BuyViewModel>();
        services.AddSingleton<PortfolioViewModel>();
        services.AddSingleton<AssetSummaryViewModel>();

        services.AddSingleton<CreateViewModel<HomeViewModel>>( services => 
        {
            return () => new HomeViewModel(
                MajorIndexViewModel.LoadMajorIndexViewModel(services.GetRequiredService<IMajorIndexService>()),
                services.GetRequiredService<AssetSummaryViewModel>()
            );
        });

        services.AddSingleton<ViewModelRenavigator<HomeViewModel>>();

        services.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
        {
            return () => new LoginViewModel(
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<ViewModelRenavigator<HomeViewModel>>()
            );
        });

        services.AddSingleton<CreateViewModel<BuyViewModel>>(services =>
        {
            return () => services.GetRequiredService<BuyViewModel>();
        });

        services.AddSingleton<CreateViewModel<PortfolioViewModel>>(services =>
        {
            return () => new PortfolioViewModel();
        });

        services.AddSingleton<INavigator, Navigator>();
        services.AddSingleton<IAuthenticator, Authenticator>();
        services.AddSingleton<IAccountStore, AccountStore>();
        services.AddSingleton<AssetStore>();

        services.AddScoped<MainViewModel>();
        services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

        return services.BuildServiceProvider();
    }
}
