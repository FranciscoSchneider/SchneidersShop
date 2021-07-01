using Ordering.Domain.Events;
using SharedKernel;
using SharedKernel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Domain
{
    public sealed class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedAt { get; }
        public int CustomerId { get; }
        public EOrderStatus Status { get; private set; }

        private List<OrderItem> _items;

        public Order(int customerId)
        {
            CustomerId = customerId;
            _items = new List<OrderItem>();
            Status = EOrderStatus.Created;
            CreatedAt = DateTime.UtcNow;
        }

        public IReadOnlyCollection<OrderItem> Items => _items?.AsReadOnly();

        public decimal TotalAmount => _items?.Sum(x => x.Quantity * x.Price) ?? 0;

        public void AddItem(OrderItem item)
        {
            _items = _items ?? new List<OrderItem>();
            _items.Add(item);
        }

        public Result Pay()
        {
            if (Status != EOrderStatus.Invoiced)
                return Result.CreateInvalidResult("Order isn't invoiced.");

            Status = EOrderStatus.Paid;
            AddDomainEvent(new PaidOrderDomainEvent(Id));
            return Result.CreateValidResult();
        }

        public Result Invoice()
        {
            if (Status != EOrderStatus.Created)
                return Result.CreateInvalidResult("Order isn't created");

            Status = EOrderStatus.Invoiced;
            AddDomainEvent(new InvoicedOrderDomainEvent(Id));
            return Result.CreateValidResult();
        }

        public Result Cancel()
        {
            if (Status == EOrderStatus.Canceled)
                return Result.CreateInvalidResult("Order is already canceled.");

            Status = EOrderStatus.Canceled;
            AddDomainEvent(new CanceledOrderDomainEvent(Id));
            return Result.CreateValidResult();
        }
    }
}
