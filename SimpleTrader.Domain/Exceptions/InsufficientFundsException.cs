﻿namespace SimpleTrader.Domain.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public double AccountBalance { get; }
        public double RequiredBalance { get; }

        public InsufficientFundsException(double accountBalance, double requiredBalance)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }

        public InsufficientFundsException(string message, double accountBalance, double requiredBalance) : base(message)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }

        public InsufficientFundsException(string message, Exception innerException, double accountBalance, double requiredBalance) : base(message, innerException)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }

    }
}
