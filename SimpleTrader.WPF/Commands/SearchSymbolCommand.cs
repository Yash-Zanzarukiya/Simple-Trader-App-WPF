using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class SearchSymbolCommand : AsyncCommandBase
    {
        private BuyViewModel _buyViewModel { get; set; }
        private readonly IStockPriceService _stockPriceService;

        public SearchSymbolCommand(BuyViewModel buyViewModel, IStockPriceService stockPriceService)
        {
            _buyViewModel = buyViewModel;
            _stockPriceService = stockPriceService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                string symbol = _buyViewModel.SearchSymbol;
                if (string.IsNullOrEmpty(symbol)) return;
                
                double stockPrice = await _stockPriceService.GetPrice(symbol);
                
                _buyViewModel.StockPrice = stockPrice;
                _buyViewModel.SearchSymbolResult = symbol;
            }
            catch(InvalidSymbolException)
            {
                _buyViewModel.ErrorMessage = "Symbol does not exist";
            }
            catch (Exception)
            {
                _buyViewModel.ErrorMessage = "Failed to get stock information";
            }
        }
    }
}
