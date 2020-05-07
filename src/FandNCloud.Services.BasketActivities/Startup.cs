using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FandNCloud.Common.Mongo;
using FandNCloud.Common.Commands;
using FandNCloud.Common.RabbitMq;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Domain.Repositories;
using FandNCloud.Services.BasketActivities.Handlers;
using FandNCloud.Services.BasketActivities.Repositories;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            services.AddMongoDB(Configuration);
            
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IBasketBlockRepository, BasketBlockRepository>();
            services.AddScoped<IDatabaseSeeder, CustomMongoSeeder>();
            services.AddRabbitMq(Configuration);
            services.AddScoped<ICommandHandler<CreateBasketActivity>, CreateBasketActivityHandler>();
            services.AddScoped<IBasketBlockService, BasketBlockService>();
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
