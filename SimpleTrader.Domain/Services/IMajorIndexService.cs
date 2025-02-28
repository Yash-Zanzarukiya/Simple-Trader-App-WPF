using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services
{
    public interface IMajorIndexService
    {
        public Task<MajorIndex> GetMajorIndex(SymbolType symbolType);
    }
}
