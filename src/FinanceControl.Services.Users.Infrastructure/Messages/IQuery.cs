using MediatR;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IQuery : IRequest
    {
    }

    public interface IQuery<out T> : IRequest<T>
    {
    }
}