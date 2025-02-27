using System;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Events;
using FandNCloud.Common.RabbitMq;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RawRabbit;

namespace FandNCloud.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public void Run() => _webHost.Run();

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            // var webHostBuilder = WebHost.CreateDefaultBuilder(args)
            //     .UseConfiguration(config)
            //     .UseStartup<TStartup>();

            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<TStartup>()
                .UseDefaultServiceProvider(options => options.ValidateScopes = false);

            return new HostBuilder(webHostBuilder.Build());
        }

        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;

            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public BusBuilder UseRabbitMq()
            {
                _bus = (IBusClient)_webHost.Services.GetService(typeof(IBusClient));

                return new BusBuilder(_webHost, _bus);
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;

            public BusBuilder(IWebHost webHost, IBusClient bus)
            {
                _webHost = webHost;
                _bus = bus;
            }

            public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
            {
                // var handler = (ICommandHandler<TCommand>)_webHost.Services
                //     .GetService(typeof(ICommandHandler<TCommand>));
                // _bus.WithCommandHandlerAsync(handler);

                // return this;
                using (var serviceScope = _webHost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var handler = (ICommandHandler<TCommand>)serviceScope.ServiceProvider.GetService(typeof(ICommandHandler<TCommand>));

                    _bus.WithCommandHandlerAsync(handler);
                    return this;
                }
            }

            public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
            {
                // var handler = (IEventHandler<TEvent>)_webHost.Services
                //     .GetService(typeof(IEventHandler<TEvent>));
                // _bus.WithEventHandlerAsync(handler);

                // return this;
                using (var serviceScope = _webHost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var handler = (IEventHandler<TEvent>)serviceScope.ServiceProvider.GetService(typeof(IEventHandler<TEvent>));

                    _bus.WithEventHandlerAsync(handler);
                    return this;
                }
            }

            public BusBuilder SubscribeToRequest<TRequest>() where TRequest : IRequest
            {
                using (var serviceScope = _webHost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var handler = (IRequestHandler<TRequest>)serviceScope.ServiceProvider.GetService(typeof(IRequestHandler<TRequest>));

                    _bus.WithRequestHandlerAsync(handler);
                    return this;
                }
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }
    }
}