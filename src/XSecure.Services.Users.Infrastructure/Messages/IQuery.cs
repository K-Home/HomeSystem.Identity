using MediatR;

namespace XSecure.Services.Users.Infrastructure.Messages
{
    public interface IQuery
    {
    }

    public interface IQuery<out T> : IRequest<T> 
    {
    }
}
