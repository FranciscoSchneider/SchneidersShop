using Ordering.Domain;
using SharedKernel;
using SharedKernel.Commands;

namespace Ordering.Application.InvoiceOrder
{
    public sealed class InvoiceOrderCommand : ICommand<Result<Order>>
    {
        public int OrderId { get; set; }
    }
}
