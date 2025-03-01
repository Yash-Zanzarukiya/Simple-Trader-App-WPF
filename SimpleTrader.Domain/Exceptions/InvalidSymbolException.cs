namespace SimpleTrader.Domain.Exceptions
{
    public class InvalidSymbolException : Exception
    {
        public InvalidSymbolException()
        {
        }

        public InvalidSymbolException(string? message) : base(message)
        {
        }
    }
}
