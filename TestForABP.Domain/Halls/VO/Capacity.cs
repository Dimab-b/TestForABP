using System;
using System.Collections.Generic;
using System.Text;

namespace TestForABP.Domain.Halls.VO
{
    public record Capacity
    {
        public int Value { get; private set; }

        public Capacity(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Місткість залу повинна бути більшою за 0.");
            }

            Value = value;
        }
    }
}
