using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain;
using SharedKernel;
using SharedKernel.Repositories;
using System;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly OrderingContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public IUnitOfWork UnitOfWork { get { return _context; } }

        public OrderRepository(OrderingContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result<Order>> AddAsync(Order order)
        {
            try
            {
                var result = await _context.Orders.AddAsync(order);
                return Result<Order>.CreateValidResult(result.Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add order ({@Order}). ", order);
                return Result<Order>.CreateInvalidResult("Failed to add order.");
            }
        }

        public async Task<Result<Order>> GetAsync(int orderId)
        {
            try
            {
                var order = await _context
                                    .Orders
                                    .FirstOrDefaultAsync(x => x.Id == orderId);
                if (order == null)
                    return Result<Order>.CreateInvalidResult($"Order {orderId} not found.");

                await _context.Entry(order).Collection(x => x.Items).LoadAsync();
                return Result<Order>.CreateValidResult(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get order {OrderId}. ", orderId);
                return Result<Order>.CreateInvalidResult($"Failed to get order {orderId}.");
            }
        }

        public Result Update(Order order)
        {
            try
            {
                _context.Entry(order).State = EntityState.Modified;
                return Result.CreateValidResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update order ({@Order}). ", order);
                return Result.CreateInvalidResult("Failed to update order.");
            }
        }
    }
}
