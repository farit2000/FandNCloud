using System.Threading.Tasks;

namespace FandNCloud.Common.Events
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}