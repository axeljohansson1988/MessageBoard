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

            services.AddControllers(options =>
                options.Filters.Add(new HttpResponseExceptionFilter())
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MessageBoard.API", Version = "v1" });
            });

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
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
