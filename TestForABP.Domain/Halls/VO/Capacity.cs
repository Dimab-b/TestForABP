using System;
using System.Collections.Generic;
using System.Text;

namespace TestForABP.Domain.Halls.VO
{
    public record Capacity
    {
        public int Value { get; private set; }

        private Capacity(int value)
        {
            Value = value;
        }

        public static Capacity Create(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Місткість залу повинна бути більшою за 0.");
            }

            return new Capacity(value);
        }
    }
}
