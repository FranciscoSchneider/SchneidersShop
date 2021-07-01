using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeName = request.GetType().Name;
            var correlationId = Guid.NewGuid();

            _logger.LogInformation("{CorrelationId}: Handling command {CommandName} ({@Command})", correlationId, typeName, request);
            var response = await next();
            _logger.LogInformation("{CorrelationId}: Command {CommandName} handled - response: {@Response}", correlationId, typeName, response);
            return response;
        }
    }
}
