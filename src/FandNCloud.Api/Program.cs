using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FandNCloud.Common.Events;
using FandNCloud.Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FandNCloud.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToEvent<BasketActivityCreated>()
                .Build()
                .Run();
        }
    }
}
