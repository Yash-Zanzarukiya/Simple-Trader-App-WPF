using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinanceAPI.Results;

namespace SimpleTrader.FinanceAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        public async Task<double> GetPrice(string symbol)
        {
            string URI = $"profile/{symbol}";

            FinanceApiHttpClient client = new FinanceApiHttpClient();

            List<StockPriceResult>? apiRes = await client.GetAsync<List<StockPriceResult>>(URI);

            var stockPriceResult = apiRes?.FirstOrDefault();

            if (stockPriceResult == null || stockPriceResult.Price == 0)
                throw new ApiException("Stock price not found for the symbol: " + symbol);

            return stockPriceResult.Price;
        }
    }
}
