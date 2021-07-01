using Microsoft.Extensions.Logging;
using Ordering.Domain;
using SharedKernel;
using SharedKernel.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.InvoiceOrder
{
    public sealed class InvoiceOrderCommandHandler : ICommandHandler<InvoiceOrderCommand, Result<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<InvoiceOrderCommandHandler> _logger;

        public InvoiceOrderCommandHandler(IOrderRepository orderRepository, ILogger<InvoiceOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Result<Order>> Handle(InvoiceOrderCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (await _orderRepository.GetAsync(command.OrderId) is var order && order.IsError)
                    return order;

                if (order.Data.Invoice() is var invoiceResult && invoiceResult.IsError)
                    return Result<Order>.CreateInvalidResult(invoiceResult.Message);

                if (_orderRepository.Update(order.Data) is var orderResult && orderResult.IsError)
                    return Result<Order>.CreateInvalidResult(orderResult.Message);

                await _orderRepository.UnitOfWork.SaveAsync();
                return Result<Order>.CreateValidResult(order.Data);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to create order ({@Command}).", command);
                return Result<Order>.CreateInvalidResult($"Failed to create order. Error: {ex.Message}");
            }
        }
    }
}
