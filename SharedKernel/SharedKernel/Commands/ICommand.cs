using MediatR;

namespace SharedKernel.Commands
{
    public interface ICommand : IRequest
    { }

    public interface ICommand<TResponse> : IRequest<TResponse>
    { }
}
