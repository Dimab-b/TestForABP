using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestForABP.Domain.Common;
using TestForABP.Domain.Halls;

namespace TestForABP.Infrastructure.Persistence
{
    public class AppDbContext : DbContext , IUnitOfWork
    {
        private readonly IPublisher _publisher;
        public AppDbContext(DbContextOptions options, IPublisher publisher) : base(options) { _publisher = publisher; }

        public DbSet<Hall> Halls { get; set; } = null!;
        public DbSet<Amenity> Amenities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var entity in domainEntities)
            {
                entity.Entity.ClearDomainEvents();
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }

    }
}
