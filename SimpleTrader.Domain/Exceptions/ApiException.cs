namespace SimpleTrader.Domain.Exceptions
{

    public class ApiException(string message) : Exception(message)
    {
        public string Message { get; } = message;
    }
}
