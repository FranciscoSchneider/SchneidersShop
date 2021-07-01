using SharedKernel.Events;

namespace Ordering.Domain.Events
{
    public sealed class CanceledOrderDomainEvent : IDomainEvent
    {
        public CanceledOrderDomainEvent(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
