using System.Security.Claims;

namespace WebApi.Infrastructure;

public interface IEventProcessor
{
    Task ProcessAsync(string message);
}
