using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharedKernel.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
