using System;
using System.Collections.Generic;
using System.Text;

namespace TestForABP.Domain.Halls.VO
{
    public record Money
    {
        public decimal Amount { get; init; }
        public string Currency {get; init; } = "UAH";

        public Money(decimal amount, string currency = "UAH")
        {
            if (amount < 0)
            {
                throw new ArgumentException("Сума не може бути від'ємною.");
            }

            Amount = amount;
            Currency = currency;
        }
    }
}
