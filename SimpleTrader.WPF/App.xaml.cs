using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinanceAPI.Services;
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

        IAuthenticationService authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();

        RegistrationResult registrationResult = await authenticationService.Register("niket@gmail.com", "niket", "12345678", "12345678");

        Account account = await authenticationService.Login("niket", "12345678");

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

        services.AddSingleton<IRootViewModelFactory, RootViewModelFactory>();
        services.AddSingleton<IViewModelFactory<HomeViewModel>, HomeViewModelFactory>();
        services.AddSingleton<IViewModelFactory<PortfolioViewModel>, PortfolioViewModelFactory>();
        services.AddSingleton<IViewModelFactory<MajorIndexViewModel>, MajorIndexViewModelFactory>();

        services.AddScoped<INavigator, Navigator>();
        services.AddScoped<BuyViewModel>();
        services.AddScoped<MainViewModel>();
        services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

        return services.BuildServiceProvider();
    }
}
