using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestForABP.Domain.Booking;

namespace TestForABP.Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("bookings");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.HallId)
                .IsRequired();

            builder.Property(b => b.Status)
                .HasConversion<string>() 
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.OwnsOne(b => b.Period, periodBuilder =>
            {
                periodBuilder.Property(p => p.Start)
                    .HasColumnName("start_time")
                    .IsRequired();

                periodBuilder.Property(p => p.End)
                    .HasColumnName("end_time")
                    .IsRequired();
            });

            builder.OwnsOne(b => b.TotalCost, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                    .HasColumnName("total_cost_amount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                moneyBuilder.Property(m => m.Currency)
                    .HasColumnName("total_cost_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            builder.OwnsMany(b => b.Amenities, amenityBuilder =>
            {
                amenityBuilder.ToTable("booking_amenities");

                amenityBuilder.WithOwner().HasForeignKey("BookingId");

                amenityBuilder.HasKey("BookingId", "AmenityId");

                amenityBuilder.Property(a => a.AmenityId)
                    .HasColumnName("amenity_id");

                amenityBuilder.Property(a => a.Name)
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .IsRequired();

                amenityBuilder.OwnsOne(a => a.Price, priceBuilder =>
                {
                    priceBuilder.Property(p => p.Amount)
                        .HasColumnName("price_amount")
                        .HasPrecision(18, 2)
                        .IsRequired();

                    priceBuilder.Property(p => p.Currency)
                        .HasColumnName("price_currency")
                        .HasMaxLength(3)
                        .IsRequired();
                });
            });

            builder.Navigation(b => b.Amenities)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
            .Property(s => s.Version)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .IsRowVersion();
        }
    }
}
