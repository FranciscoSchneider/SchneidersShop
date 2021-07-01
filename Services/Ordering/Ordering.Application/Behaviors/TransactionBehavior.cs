using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Infrastructure;
using Serilog.Context;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviors
{
    public sealed class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        private readonly OrderingContext _dbContext;

        public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, OrderingContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeName = request.GetType().Name;
            var response = default(TResponse);
            try
            {
                if (_dbContext.HasActiveTransaction)
                    return await next();

                var strategy = _dbContext.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = await _dbContext.BeginTransactionAsync())
                    using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                    {
                        _logger.LogInformation("Begin transaction {TransactionId} for {CommandName} ({@Command}).", transaction.TransactionId, typeName, request);
                        response = await next();
                        _logger.LogInformation("Commit transaction {TransactionId} for {CommandName}.", transaction.TransactionId, typeName);
                        await _dbContext.CommitTransactionAsync(transaction);
                    }
                });
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to handle transaction for {CommandName} ({@Command}).", typeName, request);
                throw;
            }
        }
    }
}
