using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using FandNCloud.Common.Mongo;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Events;
using FandNCloud.Common.RabbitMq;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Domain.Repositories;
using FandNCloud.Services.BasketActivities.Handlers;
using FandNCloud.Services.BasketActivities.Repositories;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CreateFile = FandNCloud.Common.Commands.CreateFile;

namespace FandNCloud.Services.BasketActivities
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddMvc();
            services.AddControllers();
            services.AddLogging();
            services.AddMongoDB(Configuration);
            
            // services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IBasketBlockRepository, BlockRepository>();
            services.AddScoped<IBasketFileRepository, FileRepository>();
            services.AddScoped<IBasketFolderRepository, FolderRepository>();
            
            services.AddScoped<IDatabaseSeeder, CustomMongoSeeder>();
            services.AddRabbitMq(Configuration);
            services.AddSingleton(sp 
                => CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fandncloud;AccountKey=396eHJp/U4p5N9R6OHZ2vD/toYgVxm3gC67xYYQyPCugz799k3z16Ehm2DNC7dI/sxvHa3/sbxzc5xzEN6oW3A==;EndpointSuffix=core.windows.net")
                    .CreateCloudBlobClient());

            services.AddScoped<ICommandHandler<CreateBasketActivity>, CreateBasketActivityHandler>();
            services.AddScoped<ICommandHandler<CreateFile>, CreateFileHandler>();
            services.AddScoped<ICommandHandler<CreateFolder>, CreateFolderHandler>();
            services.AddScoped<ICommandHandler<DeleteFolder>, DeleteFolderHandler>();
            services.AddScoped<ICommandHandler<DeleteFile>, DeleteFileHandler>();
            
            services.AddScoped<IEventHandler<UserCreated>, CreateUserHandler>();
            

            services.AddScoped<IRequestHandler<BrowseFolderRequest>, BrowseFolderHandler>();
            services.AddScoped<IRequestHandler<SasFileReadRequest>, SasFileReadHandler>();
            services.AddScoped<IRequestHandler<SasFileAddRequest>, SasFileAddHandler>();
            services.AddScoped<IRequestHandler<SasFileDeleteRequest>, SasFileDeleteHandler>();


            services.AddScoped<IBasketBlockService, BasketBlockService>();
            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IBlobService, BlobService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();
            app.UseHttpsRedirection();
            // app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();
        }
    }
}
