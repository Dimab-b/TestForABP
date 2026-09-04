using System;
using System.Collections.Generic;
using System.Text;
using TestForABP.Domain.Common;
using TestForABP.Domain.Halls.VO;

namespace TestForABP.Domain.Booking
{
    public class Booking : Entity , IAggregateRoot
    {
        private readonly List<BookingAmenity> _amenities = new();

        public Guid Id { get; private set; }
        public Guid HallId { get; private set; }
        public DateTimeRange Period { get; private set; } = null!;
        public Money TotalCost { get; private set; } = null!;
        public BookingStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public uint Version { get; private set; }

        public IReadOnlyCollection<BookingAmenity> Amenities => _amenities.AsReadOnly();

        private Booking() { }

        private Booking(
            Guid id,
            Guid hallId,
            DateTimeRange period,
            Money totalCost,
            IEnumerable<BookingAmenity?> amenities)
        {
            Id = id;
            HallId = hallId;
            Period = period;
            TotalCost = totalCost;
            Status = BookingStatus.Confirmed;
            CreatedAt = DateTime.UtcNow;
            _amenities.AddRange(amenities);
        }

        public static Booking Create(
        Guid hallId,
        DateTimeRange period,
        Money totalCost,
        IEnumerable<BookingAmenity> amenities)
        {
            if (hallId == Guid.Empty)
                throw new ArgumentException("HallId не може бути порожнім.");

            return new Booking(
                Guid.NewGuid(),
                hallId,
                period,
                totalCost,
                amenities ?? Enumerable.Empty<BookingAmenity>());
        }

        public void Cancel()
        {
            if (Status == BookingStatus.Cancelled)
                throw new InvalidOperationException("Бронювання вже скасовано.");

            Status = BookingStatus.Cancelled;
        }
    }
}
