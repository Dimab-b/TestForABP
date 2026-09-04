using System;
using System.Collections.Generic;
using System.Text;
using TestForABP.Domain.Halls.VO;

namespace TestForABP.Domain.Booking
{
    public enum BookingStatus
    {
        Confirmed = 1,
        Cancelled = 2
    }
    public record BookingAmenity
    {
        public Guid AmenityId { get; init; }
        public string Name { get; init; } = null!;
        public Money Price { get; init; } = null!;

        private BookingAmenity() { }

        public BookingAmenity(Guid amenityId, string name, Money price)
        {
            AmenityId = amenityId;
            Name = name;
            Price = price;
        }
    }
}
