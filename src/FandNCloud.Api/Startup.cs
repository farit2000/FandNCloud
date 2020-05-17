using FandNCloud.Api.Handlers;
using FandNCloud.Api.Services;
using FandNCloud.Common.Auth;
using FandNCloud.Common.Events;
using FandNCloud.Common.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestEase;

namespace FandNCloud.Api
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
            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy", builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:8080");
                });
            });
            // services.AddControllers();
            services.AddRabbitMq(Configuration);
            services.AddJwt(Configuration);
            services.AddScoped<IEventHandler<BasketActivityCreated>, BasketActivityCreatedHandler>();
            services.AddScoped<IBasketActivitiesService>(c =>
                RestClient.For<IBasketActivitiesService>("http://localhost:5050"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            
            app.UseRouting();
            
            app.UseCors("VueCorsPolicy");
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
