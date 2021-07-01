using SharedKernel.Events;

namespace Ordering.Domain.Events
{
    public sealed class InvoicedOrderDomainEvent : IDomainEvent
    {
        public InvoicedOrderDomainEvent(long orderId)
        {
            OrderId = orderId;
        }

        public long OrderId { get; }
    }
}
