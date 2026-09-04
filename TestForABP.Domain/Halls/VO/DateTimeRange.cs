using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TestForABP.Domain.Halls.VO
{
    public record DateTimeRange
    {
        public DateTime Start { get; init; }
        public DateTime End { get; init; }

        public TimeSpan Duration => End - Start;


        public DateTimeRange (DateTime start , DateTime end)
        {
            if (start >= end)
            {
                throw new ArgumentException("Дата початку повинна бути раніше за дату закінчення.");
            }

            Start = start;
            End = end;
        }

        public bool OverlapsWith(DateTimeRange other)
        {
            return Start < other.End && End > other.Start;
        }
    }
}
