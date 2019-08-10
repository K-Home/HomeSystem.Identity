using MediatR;

namespace HomeSystem.Services.Identity.Infrastructure.Messages
{
    public interface IQuery
    {
    }

    public interface IQuery<out T> : IRequest<T> 
    {
    }
}
