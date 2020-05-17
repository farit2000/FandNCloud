using System;
using FandNCloud.Common.Auth;
using FandNCloud.Common.Commands;
using FandNCloud.Common.RabbitMq;
using FandNCloud.Common.Requests;
using FandNCloud.Services.Identity.Domain;
using FandNCloud.Services.Identity.Domain.Database;
using FandNCloud.Services.Identity.Domain.Models;
using FandNCloud.Services.Identity.Domain.Repositories;
using FandNCloud.Services.Identity.Domain.Services;
using FandNCloud.Services.Identity.Exceptions;
using FandNCloud.Services.Identity.Handlers;
using FandNCloud.Services.Identity.Repositories;
using FandNCloud.Services.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace FandNCloud.Services.Identity
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
            services.AddCors();
            services.AddMvc(options =>
                options.Filters.Add(new HttpResponseExceptionFilter()));
            // services.AddControllers();
            services.AddLogging();
            services.AddSingleton<Func<IdentityContext>>(() =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("IdentityDb"));
                return new IdentityContext(optionsBuilder.Options);
            });
            services.AddIdentity<User, IdentityRole<Guid>>().AddEntityFrameworkStores<IdentityContext>();
            services.AddScoped<IEncrypter, Encrypter>();
            services.AddJwt(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddScoped<IRefreshTokenFactory, RefreshTokenFactory>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IInvalidTokenRepository, InvalidTokenRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddScoped<ICommandHandler<LogoutUserCommand>, LogoutUserHandler>();
            
            services.AddScoped<IRequestHandler<LoginUserRequest>, LoginUserHandler>();
            services.AddScoped<IRequestHandler<RefreshUserRequest>, RefreshUserHandler>();
            services.AddScoped<IRequestHandler<IsAuthorizedRequest>, IsAuthorizedHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;

                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));
            
            app.UseHttpsRedirection();
            
            app.UseRouting();
            
            app.UseCors(builder => builder.AllowAnyOrigin());
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
