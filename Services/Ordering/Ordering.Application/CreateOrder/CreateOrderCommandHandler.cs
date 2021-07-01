using Microsoft.Extensions.Logging;
using Ordering.Domain;
using SharedKernel;
using SharedKernel.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.CreateOrder
{
    public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Result<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Result<Order>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var order = new Order(command.CustomerId);
                foreach (var item in command.Items)
                    order.AddItem(new OrderItem(item.Description, item.Quantity, item.Price));

                var orderResult = await _orderRepository.AddAsync(order);
                if (orderResult.IsError)
                    return orderResult;                
                await _orderRepository.UnitOfWork.SaveAsync();
                return Result<Order>.CreateValidResult(order);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to create order ({@Command}).", command);
                return Result<Order>.CreateInvalidResult($"Failed to create order. Error: {ex.Message}");
            }
        }
    }
}
