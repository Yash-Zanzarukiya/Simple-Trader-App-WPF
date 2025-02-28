namespace SimpleTrader.Domain.Models
{
    public enum SymbolType
    {
        AAPL,
        NVDA,
        TSLA
    }

    public class MajorIndex
    {
        public string CompanyName { get; set; }

        public double Price { get; set; }

        public double Changes { get; set; }

        public SymbolType Symbol { get; set; }
    }
}
