using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestForABP.Domain.Halls;

namespace TestForABP.Infrastructure.Persistence.Configurations
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.ToTable("halls");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.OwnsOne(h => h.Capacity, capacityBuilder =>
            {
                capacityBuilder.Property(c => c.Value)
                    .HasColumnName("capacity")
                    .IsRequired();
            });
            builder
            .Property(s => s.Version)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .IsRowVersion();

            builder.OwnsOne(h => h.BaseHourlyRate, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                    .HasColumnName("base_hourly_rate")
                    .HasPrecision(18, 2)
                    .IsRequired();

                moneyBuilder.Property(m => m.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            builder.HasMany(h => h.Amenities)
                .WithOne()
                .HasForeignKey("HallId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(h => h.Amenities)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
