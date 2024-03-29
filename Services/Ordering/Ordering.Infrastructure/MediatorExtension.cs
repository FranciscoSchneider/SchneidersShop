﻿using MediatR;
using SharedKernel;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, OrderingContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var domainEntity in domainEntities)
                domainEntity.Entity.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
