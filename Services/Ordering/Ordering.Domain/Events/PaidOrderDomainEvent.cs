using SharedKernel.Events;

namespace Ordering.Domain.Events
{
    public sealed class PaidOrderDomainEvent : IDomainEvent
    {
        public PaidOrderDomainEvent(long orderId)
        {
            OrderId = orderId;
        }

        public long OrderId { get; }
    }
}
