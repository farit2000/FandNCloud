using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Events;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Common.Services;
using FandNCloud.Services.BasketActivities.Handlers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CreateFile = FandNCloud.Common.Commands.CreateFile;

namespace FandNCloud.Services.BasketActivities
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToCommand<CreateBasketActivity>()
                .SubscribeToCommand<CreateFile>()
                .SubscribeToCommand<DeleteFile>()
                .SubscribeToCommand<CreateFolder>()
                .SubscribeToCommand<DeleteFolder>()
                .SubscribeToCommand<CreateUser>()
                
                .SubscribeToRequest<BrowseFolderRequest>()
                .SubscribeToRequest<SasFileReadRequest>()
                .SubscribeToRequest<SasFileAddRequest>()
                .SubscribeToRequest<SasFileDeleteRequest>()
                
                .SubscribeToEvent<UserCreated>()
                
                .Build()
                .Run();
        }
    }
}
