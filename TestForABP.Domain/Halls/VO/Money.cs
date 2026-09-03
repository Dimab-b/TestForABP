using System;
using System.Collections.Generic;
using System.Text;

namespace TestForABP.Domain.Halls.VO
{
    public record Money
    {
        public decimal Amount { get; private set; }
        public string Currency {get; private set; } = "UAH";

        private Money(decimal amount, string currency = "UAH")
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money Create(decimal amount, string currency = "UAH")
        {
            if (amount < 0)
            {
                throw new ArgumentException("Сума не може бути від'ємною.");
            }

            return new Money(amount, currency);
        }
    }
}
