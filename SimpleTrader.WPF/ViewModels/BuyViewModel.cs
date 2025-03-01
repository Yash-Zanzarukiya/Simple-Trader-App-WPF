using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Accounts;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class BuyViewModel : ViewModelBase
    {
		private string _searchSymbol = string.Empty;

        public string SearchSymbol
        {
			get { return _searchSymbol.ToUpper(); }
			set
			{
				_searchSymbol = value;
				OnPropertyChanged(nameof(SearchSymbol));
			}
		}

		private string _searchSymbolResult = string.Empty;
		public string SearchSymbolResult
		{
			get { return _searchSymbolResult; }
			set
			{
				_searchSymbolResult = value;
				OnPropertyChanged(nameof(SearchSymbolResult));
			}
		}

		private double _stockPrice;
		public double StockPrice
		{
			get { return _stockPrice; }
			set
			{
				_stockPrice = value;
				OnPropertyChanged(nameof(StockPrice));
                OnPropertyChanged(nameof(TotalPrice));
            }
		}

		private int _sharesToBuy;
		public int SharesToBuy
        {
			get { return _sharesToBuy; }
			set
			{
				_sharesToBuy = value;
				OnPropertyChanged(nameof(SharesToBuy));
                OnPropertyChanged(nameof(TotalPrice));
            }
		}

		public MessageViewModel ErrorMessageViewModel { get; }
		public string ErrorMessage
        {
            set { ErrorMessageViewModel.Message = value; }
        }

        public MessageViewModel StatusMessageViewModel { get; }
        public string StatusMessage
        {
            set { StatusMessageViewModel.Message = value; }
        }

        public double TotalPrice { get { return SharesToBuy * StockPrice; } }

        public ICommand SearchSymbolCommand { get; set; }
        public ICommand BuyStockCommand { get; set; }

        public BuyViewModel(IStockPriceService stockPriceService, IBuyStockService buyStockService , IAccountStore accountStore)
        {
            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();

            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService, accountStore);
        }
    }
}
