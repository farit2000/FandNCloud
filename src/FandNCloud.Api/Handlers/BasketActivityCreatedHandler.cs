using System;
using System.Threading.Tasks;
using FandNCloud.Common.Events;

namespace FandNCloud.Api.Handlers
{
    public class BasketActivityCreatedHandler : IEventHandler<BasketActivityCreated>
    {
        public async Task HandleAsync(BasketActivityCreated @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Activity created: {@event.ItemName}" + " " + $"Time is: {DateTime.Now}" + " " + $"Microsec: {DateTime.Now.Millisecond}");
        }
    }
}