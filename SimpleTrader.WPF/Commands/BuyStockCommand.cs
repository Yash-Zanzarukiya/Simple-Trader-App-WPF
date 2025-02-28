using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class BuyStockCommand : ICommand
    {
        private BuyViewModel _buyViewModel { get; set; }
        private readonly IBuyStockService _buyStockService;
        private readonly IDataService<Account> _accountService;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService, IDataService<Account> accountService)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
            _accountService = accountService;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                string symbol = _buyViewModel.SearchSymbol;
                int shares = _buyViewModel.SharesToBuy;
                Account account = await _accountService.GetById(1);

                Account buyer = await _buyStockService.BuyStock(account, symbol, shares);

                _buyViewModel.SharesToBuy = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
