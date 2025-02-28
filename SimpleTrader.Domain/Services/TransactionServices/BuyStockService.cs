using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public class BuyStockService : IBuyStockService
    {
        private readonly IDataService<Account> _accountService;
        private readonly IStockPriceService _stockPriceService;

        public BuyStockService(IDataService<Account> accountService, IStockPriceService stockPriceService)
        {
            _accountService = accountService;
            _stockPriceService = stockPriceService;
        }

        public async Task<Account> BuyStock(Account buyer, string symbol, int shares)
        {
            ArgumentNullException.ThrowIfNull(buyer);
            if (shares <= 0) throw new ArgumentException("Shares must be greater than 0", nameof(shares));
            if (string.IsNullOrEmpty(symbol)) throw new ArgumentException("Symbol must be provided", nameof(symbol));

            double stockPrice = await _stockPriceService.GetPrice(symbol);

            double transactionCost = stockPrice * shares;
            
            if (transactionCost > buyer.Balance)
            {
                throw new InsufficientFundsException(buyer.Balance, transactionCost);
            }

            buyer.Balance -= transactionCost;

            buyer.AssetTransactions.Add(new AssetTransaction
            {
                Account = buyer,
                Asset = new Asset
                {
                    Symbol = symbol,
                    PricePerShare = stockPrice
                },
                IsPurchase = true,
                Shares = shares,
                DateProcessed = DateTime.Now
            });


            await _accountService.Update(buyer.Id, buyer);
            return buyer;
        }
    }
}
