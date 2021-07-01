using Ordering.Domain;
using SharedKernel;
using SharedKernel.Commands;
using System.Collections.Generic;

namespace Ordering.Application.CreateOrder
{
    public sealed class CreateOrderCommand : ICommand<Result<Order>>
    {
        public int CustomerId { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }

        public sealed class OrderItem
        {
            public string Description { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
}
