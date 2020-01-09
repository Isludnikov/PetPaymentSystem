using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetPaymentSystem.Middleware;
using PetPaymentSystem.Models.Generated;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using Microsoft.AspNetCore.Mvc;
using PetPaymentSystem.Factories;
using PetPaymentSystem.Filter;
using PetPaymentSystem.Helpers;
using PetPaymentSystem.Services;

namespace PetPaymentSystem
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
            services.AddDbContext<PaymentSystemContext>(options => options
                .UseMySql(Configuration.GetConnectionString("MySql"), mySqlOptions => mySqlOptions
                    .ServerVersion(new ServerVersion(new Version(8, 0, 18)))
                ));
            services
                .AddControllers(options =>
                    options.Filters.Add(
                        new HttpResponseExceptionFilter())
                    )
                .ConfigureApiBehaviorOptions(options =>
                    {
                        options.InvalidModelStateResponseFactory = context => context.HttpContext.Request.Path.Value.StartsWith("/api") 
                            ? new BadRequestObjectResult(ModelValidationHelper.Validate(context.ModelState)) 
                            : new BadRequestObjectResult(context.ModelState);
                    })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });
            services.AddScoped<MerchantManagerService>();
            services.AddScoped<SessionManagerService>();
            services.AddScoped<OperationManagerService>();
            services.AddScoped<ProcessingFactory>();
            services.AddScoped<TerminalSelectorService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseMiddleware<ApiLoggingMiddleware>();
            app.UseMiddleware<ApiAuthenticationMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
