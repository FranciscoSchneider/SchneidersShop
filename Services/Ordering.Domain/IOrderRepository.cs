using SharedKernel;
using SharedKernel.Repositories;
using System.Threading.Tasks;

namespace Ordering.Domain
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Result<Order>> AddAsync(Order order);
        Result Update(Order order);
        Task<Result<Order>> GetAsync(int orderId);
    }
}
