using MediatR;

namespace SharedKernel.Commands
{
    public interface ICommandHandler<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest<Unit>
    { }

    public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    { }
}
