using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class BuyStockCommand : AsyncCommandBase
    {
        private readonly BuyViewModel _buyViewModel;
        private readonly IBuyStockService _buyStockService;
        private readonly IAccountStore _accountStore;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService, IAccountStore accountStore)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
            _accountStore = accountStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _buyViewModel.StatusMessage = string.Empty;
            _buyViewModel.ErrorMessage = string.Empty;

            try
            {
                string symbol = _buyViewModel.SearchSymbol;
                int shares = _buyViewModel.SharesToBuy;

                Account account = _accountStore.CurrentAccount;

                Account buyer = await _buyStockService.BuyStock(account, symbol, shares);

                _accountStore.CurrentAccount = buyer;

                _buyViewModel.StatusMessage = $"Congratulations! You bought {shares} shares of {symbol} at ${_buyViewModel.StockPrice} per share.";
            }
            catch (InvalidSymbolException)
            {
                _buyViewModel.ErrorMessage = "Symbol does not exist.";
            }
            catch (InsufficientFundsException)
            {
                _buyViewModel.ErrorMessage = "Account has insufficient funds.";
            }
            catch (Exception)
            {
                _buyViewModel.ErrorMessage = "Transaction failed.";
            }
        }
    }
}
