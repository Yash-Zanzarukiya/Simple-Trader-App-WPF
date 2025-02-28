using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.FinanceAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        public async Task<MajorIndex> GetMajorIndex(SymbolType symbolType)
        {
            string URI = $"profile/{symbolType}";

            using FinanceApiHttpClient client = new FinanceApiHttpClient();

            List<MajorIndex> apiRes = await client.GetAsync<List<MajorIndex>>(URI);

            return apiRes?.FirstOrDefault() ?? throw new ApiException("Major Index Not Found");
        }
    }
}
