using System.Reflection;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Events;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;

namespace FandNCloud.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus,
            ICommandHandler<TCommand> handler) where TCommand : ICommand
            => bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                    cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus,
            IEventHandler<TEvent> handler) where TEvent : IEvent
            => bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                    cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));

        public static Task WithRequestHandlerAsync<TRequest>(this IBusClient busClient,
            IRequestHandler<TRequest> requestHandler) where TRequest : IRequest
            => busClient.RespondAsync<TRequest, IRespond>(msg => requestHandler.HandleAsync(msg),
                ctx => ctx.UseRespondConfiguration(cfg => cfg.FromDeclaredQueue(q =>
                    q.WithName(GetQueueName<TRequest>()))));

        private static string GetQueueName<T>()
            => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitMqOptions();
            var section = configuration.GetSection("rabbitmq");
            section.Bind(options);
            var cl = RawRabbitFactory.CreateSingleton();
            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });
            services.AddSingleton<IBusClient>(_ => client);
        }
    }
}