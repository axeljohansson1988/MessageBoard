using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MessageBoard.API.ErrorHandling;
using MessageBoard.API.Providers;
using MessageBoard.API.Providers.Interfaces;
using MessageBoard.API.Services;
using MessageBoard.API.Services.Interfaces;

namespace MessageBoard.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowOrigin", builder => builder.WithOrigins().AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            services.AddControllers(options =>
                options.Filters.Add(new HttpResponseExceptionFilter())
            );
            
            this.AddSwagger(services);
            this.AddServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MessageBoard.API v1"));
            }

            app.UseRouting();

            app.UseCors("AllowOrigin");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        } 

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Message Board API",
                    Description = "An API for adding, editing and deleting messages on a message board. Supports multiple clients.",
                    Contact = new OpenApiContact
                    {
                        Name = "Axel Johansson",
                    }
                });
            });
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IBoardMessageService, BoardMessageService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IIdProvider, IdProvider>();
        }
    }
}
