using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class SearchSymbolCommand : ICommand
    {
        private BuyViewModel _buyViewModel { get; set; }
        private readonly IStockPriceService _stockPriceService;

        public event EventHandler? CanExecuteChanged;

        public SearchSymbolCommand(BuyViewModel buyViewModel, IStockPriceService stockPriceService)
        {
            _buyViewModel = buyViewModel;
            _stockPriceService = stockPriceService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                string symbol = _buyViewModel.SearchSymbol;
                if (string.IsNullOrEmpty(symbol)) return;
                double stockPrice = await _stockPriceService.GetPrice(symbol);
                _buyViewModel.StockPrice = stockPrice;
                _buyViewModel.SearchSymbolResult = symbol;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
