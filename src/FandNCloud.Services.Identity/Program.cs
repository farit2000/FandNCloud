using FandNCloud.Common.Commands;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Services;

namespace FandNCloud.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToCommand<LogoutUserCommand>()
                
                .SubscribeToRequest<LoginUserRequest>()
                .SubscribeToRequest<RefreshUserRequest>()
                .SubscribeToRequest<IsAuthorizedRequest>()
                .SubscribeToRequest<CreateUserRequest>()
                
                .Build()
                .Run();
        }
    }
}
